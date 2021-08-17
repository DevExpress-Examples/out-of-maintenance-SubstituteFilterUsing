Imports DevExpress.Data.Filtering
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls

Namespace WebApplication1
	Partial Public Class [Default]
		Inherits System.Web.UI.Page

		Protected Sub Page_Init(ByVal sender As Object, ByVal e As EventArgs)
			GridView1.DataSource = New List(Of MyDataRow) From {
				New MyDataRow With {
					.Id = 1,
					.Title = "TitleA"
				},
				New MyDataRow With {
					.Id = 2,
					.Title = "",
					.Notes = "This title has empty string value"
				},
				New MyDataRow With {
					.Id = 3,
					.Title = Nothing,
					.Notes = "This title has NULL value"
				}
			}

			GridView1.DataBind()
		End Sub

		Public Class MyDataRow
			Public Property Id() As Integer
			Public Property Title() As String
			Public Property Notes() As String
		End Class



		Protected Sub GridView1_SubstituteFilter(ByVal sender As Object, ByVal e As DevExpress.Data.SubstituteFilterEventArgs)
			Dim cols = CriteriaColumnAffinityResolver.SplitByColumns(e.Filter)
			Dim isEqualsOperator = cols.Select(Function(a) a.Value).OfType(Of BinaryOperator)().Where(Function(uo) uo.OperatorType = BinaryOperatorType.Equal AndAlso (TryCast(uo.RightOperand, OperandValue)).Value Is Nothing).ToList()
			If isEqualsOperator.Count <> 0 Then
				isEqualsOperator.ForEach(Sub(i)
					Dim op As OperandProperty = (TryCast(i.LeftOperand, OperandProperty))
					Dim fn As String = op.PropertyName
					Dim nbo As New BinaryOperator(fn, String.Empty, BinaryOperatorType.Equal)
					cols(op) = GroupOperator.Or(New CriteriaOperator() { nbo, cols(op) })
				End Sub)
			End If
			e.Filter = GroupOperator.And(cols.Values)
		End Sub
	End Class
End Namespace