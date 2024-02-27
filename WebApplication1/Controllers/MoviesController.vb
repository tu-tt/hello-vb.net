Imports System.Data.Entity
Imports System.Net

Namespace Controllers
    Public Class MoviesController
        Inherits Controller

        Private db As New AppDbContext

        ' GET: Movies
        Function Index() As ActionResult
            Return View(db.Movies.ToList())
        End Function

        ' GET: Movies/Details/5
        Function Details(ByVal id As Integer?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim movie As Movie = db.Movies.Find(id)
            If IsNothing(movie) Then
                Return HttpNotFound()
            End If
            Return View(movie)
        End Function

        ' GET: Movies/Create
        Function Create() As ActionResult
            Return View()
        End Function

        ' POST: Movies/Create
        'To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        'more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        <HttpPost()>
        <ValidateAntiForgeryToken()>
        Function Create(<Bind(Include:="ID,Title,ReleaseDate,Genre,Price")> ByVal movie As Movie) As ActionResult
            If ModelState.IsValid Then
                db.Movies.Add(movie)
                db.SaveChanges()
                Return RedirectToAction("Index")
            End If
            Return View(movie)
        End Function

        ' GET: Movies/Edit/5
        Function Edit(ByVal id As Integer?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim movie As Movie = db.Movies.Find(id)
            If IsNothing(movie) Then
                Return HttpNotFound()
            End If
            Return View(movie)
        End Function

        ' POST: Movies/Edit/5
        'To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        'more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        <HttpPost()>
        <ValidateAntiForgeryToken()>
        Function Edit(<Bind(Include:="ID,Title,ReleaseDate,Genre,Price")> ByVal movie As Movie) As ActionResult
            If ModelState.IsValid Then
                db.Entry(movie).State = EntityState.Modified
                db.SaveChanges()
                Return RedirectToAction("Index")
            End If
            Return View(movie)
        End Function

        ' GET: Movies/Delete/5
        Function Delete(ByVal id As Integer?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim movie As Movie = db.Movies.Find(id)
            If IsNothing(movie) Then
                Return HttpNotFound()
            End If
            Return View(movie)
        End Function

        ' DELETE: Movies/Delete/5
        <HttpDelete()>
        <ActionName("Delete")>
        <ValidateAntiForgeryToken()>
        Function DeleteConfirmed(ByVal id As Integer) As ActionResult
            Dim movie As Movie = db.Movies.Find(id)
            db.Movies.Remove(movie)
            db.SaveChanges()
            Return RedirectToAction("Index")
        End Function

        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            If (disposing) Then
                db.Dispose()
            End If
            MyBase.Dispose(disposing)
        End Sub
    End Class
End Namespace
