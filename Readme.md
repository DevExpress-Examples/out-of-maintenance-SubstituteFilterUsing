# GridView for WebForms - How to change the "Equals to Null" condition to "Equals to Null or Empty" in the GridView filter expression

<p>GridView FilterBuilder offers the Equals operator. When an end-user selects this operator for the string column and assigns nothing to the right operand, only values that are equal to null are displayed. </p> 
<p>If you need to display values that are equal to null or empty string, you can handle the <a href="https://docs.devexpress.com/AspNet/DevExpress.Web.ASPxGridBase.SubstituteFilter">ASPxGridView.SubstitureFilter</a> event and change the "Equals to Null" condition to the "Equals to Null or Empty" condition. </p>
<p>In the SubstitureFilter event handler, you need to use the CriteriaColumnAffinityResolver.SplitByColumns method to parse the Filter value. After this, find all BinaryOperators whose type is Equal and the right operand value is null. Then, for these operators, add an additional binary operator with an empty string.</p>

```cs
using DevExpress.Data.Filtering;
...
protected void GridView1_SubstituteFilter(object sender, DevExpress.Data.SubstituteFilterEventArgs e) {
    var cols = CriteriaColumnAffinityResolver.SplitByColumns(e.Filter);
    var isEqualsOperator = cols.Select(a => a.Value).OfType<BinaryOperator>().Where(uo => uo.OperatorType == BinaryOperatorType.Equal && (uo.RightOperand as OperandValue).Value == null).ToList();
    if (isEqualsOperator.Count != 0) {
        isEqualsOperator.ForEach(i => {
            OperandProperty op = (i.LeftOperand as OperandProperty);
            string fn = op.PropertyName;
            BinaryOperator nbo = new BinaryOperator(fn, string.Empty, BinaryOperatorType.Equal);
            cols[op] = GroupOperator.Or(new CriteriaOperator[] { nbo, cols[op] });
        });
    }
    e.Filter = GroupOperator.And(cols.Values);
}
```


In the sample, the Title filed contains null and string.Empty values. Both values are shown after handling the SubstitureFilter event.

**See also:**

<a href="https://www.devexpress.com/Support/Center/p/ka18784">ASPxGridView - How to programmatically change the column's filter in the FilterExpression</a> 
<a href="https://docs.devexpress.com/CoreLibraries/4928/devexpress-data-library/criteria-language-syntax">Criteria Language Syntax</a> 

<br/>
<!-- default file list -->
Files to look at:

* [Default.aspx](./CS/SubstituteFilterUsing/Default.aspx) (VB: [Default.aspx](./VB/SubstituteFilterUsing/Default.aspx))
* [Default.aspx.cs](./CS/SubstituteFilterUsing/Default.aspx.cs) (VB: [Default.aspx.vb](./VB/SubstituteFilterUsing/Default.aspx.vb))
<!-- default file list end -->


