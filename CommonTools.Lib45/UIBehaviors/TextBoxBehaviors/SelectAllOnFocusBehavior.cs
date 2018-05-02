using System.Windows.Controls;
using System.Windows.Interactivity;

namespace CommonTools.Lib45.UIBehaviors.TextBoxBehaviors
{
    public class SelectAllOnFocusBehavior : Behavior<TextBox>
    {
        protected override void OnAttached()
        {
            AssociatedObject.GotFocus += (s, e)
                => AssociatedObject.SelectAll();
        }
    }
}
