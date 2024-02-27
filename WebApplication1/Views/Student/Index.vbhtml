@ModelType IEnumerable(Of WebApplication1.Student)
@Code
    ViewData("Title") = "Student"
    Layout = "~/Views/Shared/_Layout.vbhtml"
End Code

<h2 class="mt-2">Student List</h2>
<section>
    <button class="btn btn-primary my-2" id="create-btn">New</button>
</section>
<section>
    <table class="table table-hover w-75">
        <thead>
            <tr>
                <th class="w-25">Name</th>
                <th class="w-25">Age</th>
                <th class="w-25">Actions</th>
            </tr>
        </thead>
        <tbody>
            @Code
                For i As Integer = 0 To Model.Count - 1
                    Dim student As Student = Model.ElementAt(i)
                    @<tr id="student-@i">
                        <td>
                            @Html.DisplayFor(Function(modelItem) student.Name)
                        </td>
                        <td>
                            @Html.DisplayFor(Function(modelItem) student.Age)
                        </td>
                        <td class="d-flex gap-2 py-1">
                            <button class="btn btn-outline-primary btn-score d-inline-flex align-items-center gap-2" 
                                    style="width: 140px" data-student-id="@student.ID">
                                <i class="fa fa-table" aria-hidden="true"></i> Score Table
                            </button>
                            <button class="btn btn-primary btn-edit" data-student-id="@student.ID">
                                <i class="fa fa-pencil" aria-hidden="true"></i>
                            </button>
                            <button class="btn btn-danger btn-delete" data-student-id="@student.ID">
                                <i class="fa fa-trash" aria-hidden="true"></i>
                            </button>
                        </td>
                    </tr>
                Next i
            End Code
        </tbody>
    </table>

    <!-- Score Table Modal -->
    <div class="modal fade" id="score-modal" tabindex="-1" aria-labelledby="score-modal-label" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h1 class="modal-title fs-5" id="score-modal-label">Student Score Table</h1>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <table class="table table-hover">
                        <thead>
                            <tr>
                                <th>Subject</th>
                                <th>Score</th>
                            </tr>
                        </thead>
                        <tbody id="score-tbody">
                        </tbody>
                    </table>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-primary" data-bs-dismiss="modal">Close</button>
                </div>
            </div>
        </div>
    </div>

    <!-- Edit Score Modal -->
    <div class="modal fade" id="edit-score-modal" tabindex="-1" aria-labelledby="edit-score-modal-label" aria-hidden="true">
        <div class="modal-dialog">
            <form class="modal-content" action="/Student/EditScore" method="post" id="edit-score-form">
                @Html.AntiForgeryToken()
                <input type="hidden" id="score-id-input" name="StudentId" />
                <div class="modal-header">
                    <h1 class="modal-title fs-5" id="edit-score-modal-label">Student Score Table</h1>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <table class="table table-hover">
                        <thead>
                            <tr>
                                <th class="w-50">Subject</th>
                                <th class="w-50">Score</th>
                            </tr>
                        </thead>
                        <tbody id="edit-score-tbody"></tbody>
                    </table>
                    <button type="button" class="btn btn-primary" id="add-score-btn">Add New Score</button>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>
                    <button type="submit" class="btn btn-primary">Save changes</button>
                </div>
            </form>
        </div>
    </div>

    <!-- Student Info Modal -->
    <div class="modal fade" id="student-modal" tabindex="-1" aria-labelledby="student-modal-label" aria-hidden="true">
        <div class="modal-dialog">
            <form class="modal-content" action="/Student/Create" method="post" id="student-form">
                @Html.AntiForgeryToken()
                <input type="hidden" id="id-input" name="ID" />
                <div class="modal-header">
                    <h1 class="modal-title fs-5" id="student-modal-label">Edit Student Info</h1>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <div>
                        <div class="mb-3">
                            <label for="name-input" class="form-label">Name:</label>
                            <input class="form-control" id="name-input" name="name">
                        </div>
                        <div class="mb-3">
                            <label for="age-input" class="form-label">Age:</label>
                            <input class="form-control" id="age-input" name="age">
                        </div>
                        <div class="mb-3">
                            <button type="button" class="btn btn-primary" id="edit-score-btn">Edit Student Score</button>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cancel</button>
                    <button type="submit" class="btn btn-primary">Save changes</button>
                </div>
            </form>
        </div>
    </div>

    <!-- Delete Student Modal -->
    <div class="modal fade" id="delete-student-modal" tabindex="-1" aria-labelledby="delete-student-modal-label" aria-hidden="true">
        <div class="modal-dialog">
            <form class="modal-content" action="/Student/Delete" method="post" id="delete-student-form">
                @Html.AntiForgeryToken()
                <input type="hidden" id="delete-id-input" name="ID" />
                <div class="modal-header">
                    <h1 class="modal-title fs-5" id="delete-student-modal-label">Delete Student</h1>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <p>Are you sure that you want to delete student <b id="student-name"></b>?</p>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cancel</button>
                    <button type="submit" class="btn btn-danger">Delete</button>
                </div>
            </form>
        </div>
    </div>
    
</section>

@Section Scripts
    <script>
        const students = [
            @Code
                For Each student In Model
                    Dim scs As String = "["
                    If (student.Scores IsNot Nothing) Then
                        For Each score In student.Scores
                            If (score.Subject IsNot Nothing) Then
                                Dim obj As String = "{"
                                obj = obj & "subject: " & score.Subject.Id & ","
                                obj = obj & "value: " & score.Score & ","
                                obj = obj & "}"

                                scs = scs & obj & ","
                            End If
                        Next
                    End If
                    scs = scs & "]"

                    Dim str As String = "{"
                    str = str & "id: " & student.Id & ","
                    str = str & "name: '" & student.Name & "',"
                    str = str & "age: " & student.Age & ","
                    str = str & "scores: " & scs & ","
                    str = str & "},"

                    @Html.Raw(str)  
                Next
            End Code
        ]

        const subjectMap = {
            @Code
                For Each subject In ViewBag.SubjectList
                    Dim str = subject.ID & ": '" & subject.Name & "',"
                    @Html.Raw(str)
                Next
            End Code
        }

        const scoreTBody = u('#score-tbody')
        const editScoreTBody = u('#edit-score-tbody')
        const form = u('#student-form')
        const deleteForm = u('#delete-student-form')

        const modalOptions = { keyboard: false, focus: false }
        const modal = new bootstrap.Modal(u('#student-modal').first(), modalOptions)
        const scoreModal = new bootstrap.Modal(u('#score-modal').first(), modalOptions)
        const editScoreModal = new bootstrap.Modal(u('#edit-score-modal').first(), modalOptions)
        const deleteModal = new bootstrap.Modal(u('#delete-student-modal').first(), modalOptions)

        const openForm = event => {
            let el = u(event.target)
            let student = null;

            if (el.is('i')) el = el.parent()
            if (el.is('button') && el.hasClass('btn-edit')) {
                const studentId = parseInt(el.data('student-id'))
                student = students.find(s => s.id === studentId)
                
                form.attr('action', '/Student/Edit')
            } else form.attr('action', '/Student/Create')
           
            form.find('#id-input').first().value = student?.id || ''
            form.find('#name-input').first().value = student?.name || ''
            form.find('#age-input').first().value = student?.age || ''

            modal.show()
        }

        const openDeleteForm = event => {
            let el = u(event.target)
            if (el.is('i')) el = el.parent()
            const studentId = parseInt(el.data('student-id'))
            const student = students.find(s => s.id === studentId)

            deleteForm.find('#delete-id-input').first().value = student.id
            deleteForm.find('#student-name').html(student.name)
            deleteModal.show()
        }

        const showStudentScore = event => {
            let el = u(event.target)
            if (el.is('i')) el = el.parent()
            const studentId = parseInt(el.data('student-id'))
            const student = students.find(s => s.id === studentId)

            scoreTBody.html(null)
            student.scores.forEach(score => {
                const subjectTd = u('<td>').text(subjectMap[score.subject])
                const scoreTd = u('<td>').text(score.value)
                const row = u('<tr>').append(subjectTd).append(scoreTd)
                scoreTBody.append(row)
            })

            u('#score-modal-label').text(student.name + '\'s Scores')
            scoreModal.show()
        }

        const editStudentScore = event => {
            const id = parseInt(u('#id-input').attr('value'))
            const student = students.find(s => s.id === id)
            u('#score-id-input').attr('value', id)

            editScoreTBody.html(null)
            for (const score of student.scores) {
                if (!score) continue

                const selectedValue = score.subject 
                const select = u('<select>')
                    .append(genOption(1, 'Math', selectedValue))
                    .append(genOption(2, 'Physics', selectedValue))
                    .append(genOption(3, 'Vietnamese', selectedValue))
                    .addClass('form-select')
                    .attr('name', 'SubjectId')
                    .attr('value', score.subject)
                const subjectTd = u('<td>').append(select)

                const scoreInput = u('<input>')
                    .attr('name', 'Value')
                    .attr('type', 'number')
                    .attr('max', 10)
                    .attr('value', score.value)
                    .addClass('form-control')
                const scoreTd = u('<td>').append(scoreInput)

                const row = u('<tr>').append(subjectTd).append(scoreTd)
                editScoreTBody.append(row)
            }

            modal.hide()
            editScoreModal.show()
        }

        const addStudentScore = event => {
            const select = u('<select>')
                .append(genOption(1, 'Math'))
                .append(genOption(2, 'Physics'))
                .append(genOption(3, 'Vietnamese'))
                .addClass('form-select')
                .attr('name', 'SubjectId')
            const subjectTd = u('<td>').append(select)

            const scoreInput = u('<input>')
                .attr('name', 'Value')
                .attr('type', 'number')
                .attr('max', 10)
                .addClass('form-control')
            const scoreTd = u('<td>').append(scoreInput)
            const row = u('<tr>').append(subjectTd).append(scoreTd)

            editScoreTBody.append(row)
        }

        const submitScores = async event => {
            const form = editScoreForm.first()
            const scores = []

            let formData = new FormData(form);
            let data = new FormData();
            let obj = {}
            for (const [key, value] of formData) {
                switch (key) {
                    case 'SubjectId':
                    case 'Value':
                        obj[key] = parseInt(value)
                        if ('SubjectId' in obj && 'Value' in obj) {
                            scores.push(obj)
                            obj = {}
                        }
                        break
                    default:
                        data.set(key, value)
                }
            }

            const scoresInput = u('<input>')
                .attr('type', 'hidden')
                .attr('name', 'Scores')
                .attr('value', JSON.stringify(scores))
            form.appendChild(scoresInput.first())
            form.submit()
        }

        // Utilities
        const genOption = (optionValue, optionLabel, selectedValue) => {
            return u('<option>').text(optionLabel)
                .attr('value', optionValue)
                .attr('selected', selectedValue === optionValue)
        }

        // Finalize Setup
        u('#create-btn').on('click', openForm)
        u('#edit-score-btn').on('click', editStudentScore)
        u('#add-score-btn').on('click', addStudentScore)

        u('.btn-edit').each(el => u(el).on('click', openForm))
        u('.btn-delete').each(el => u(el).on('click', openDeleteForm))
        u('.btn-score').each(el => u(el).on('click', showStudentScore))
        
        const editScoreForm = u('#edit-score-form').handle('submit', submitScores)
    </script>
End Section
