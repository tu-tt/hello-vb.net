Imports System.Data.Entity
Imports Newtonsoft.Json

Namespace Controllers
    Public Class StudentController
        Inherits Controller

        Private db As New AppDbContext

        ' GET: Student
        Function Index() As ActionResult
            ViewData("SubjectList") = db.Subjects.ToList()
            Return View(db.Students.Include("Scores").Include("Scores.Subject").ToList())
        End Function

        ' POST: Student/Create
        <HttpPost()>
        <ValidateAntiForgeryToken()>
        Function Create(<Bind(Include:="Name,Age")> ByVal student As Student) As ActionResult
            If ModelState.IsValid Then
                Dim score As StudentScore = New StudentScore()
                score.Student = student
                score.Score = 7

                Dim scores As New List(Of StudentScore)({score})
                student.Scores = scores

                db.Students.Add(student)
                db.SaveChanges()
            End If
            Return RedirectToAction("Index")
        End Function

        ' POST: Student/Edit
        'To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        'more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        <HttpPost()>
        <ValidateAntiForgeryToken()>
        Function Edit(<Bind(Include:="ID,Name,Age")> ByVal student As Student) As ActionResult
            If ModelState.IsValid Then
                db.Entry(student).State = EntityState.Modified
                db.SaveChanges()
            End If
            Return RedirectToAction("Index")
        End Function

        ' POST: Student/EditScore
        'To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        'more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        <HttpPost()>
        <ValidateAntiForgeryToken()>
        Function EditScore(ByVal scoreForm As EditScoreForm) As ActionResult
            ' Map to StudentScore
            Dim scoreList As New List(Of StudentScore)()
            Dim scores = JsonConvert.DeserializeObject(Of List(Of ScoreObject))(scoreForm.Scores)
            Dim student = db.Students.Find(scoreForm.StudentId)

            ' Update StudentScores
            Dim query = From sc In db.StudentScores
                        Where sc.Student.Id = student.Id
                        Select sc
            Dim studentScores = query.ToList()
            db.StudentScores.RemoveRange(studentScores)

            For Each score In scores
                Dim obj As New StudentScore
                obj.Subject = db.Subjects.Find(score.SubjectId)
                obj.Student = student
                obj.Score = score.Value

                db.StudentScores.Add(obj)
            Next

            db.SaveChanges()

            Return RedirectToAction("Index")
        End Function

        <HttpPost()>
        <ValidateAntiForgeryToken()>
        Function Delete(ByVal ID As Integer) As ActionResult
            Dim student As Student = db.Students.Find(ID)
            Dim scores = (From sc In db.StudentScores
                          Where sc.Student.Id = student.Id
                          Select sc).ToList()

            db.StudentScores.RemoveRange(scores)
            db.Students.Remove(student)
            db.SaveChanges()
            Return RedirectToAction("Index")
        End Function
    End Class
End Namespace