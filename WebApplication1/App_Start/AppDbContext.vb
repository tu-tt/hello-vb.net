Imports System.Data.Entity

Public Class AppDbContext
    Inherits DbContext

    Public Sub New()
        MyBase.New("Server=localhost,1433;UID=sa;PWD=P@ssw0rd;")
    End Sub

    Public Property Movies() As DbSet(Of Movie)
    Public Property Students() As DbSet(Of Student)
    Public Property StudentScores() As DbSet(Of StudentScore)
    Public Property Subjects() As DbSet(Of Subject)
End Class
