namespace MealPlannerClient.App.Behaviors
{
    public class PositiveNumberValidationBehavior : Behavior<Entry>
    {
        protected override void OnAttachedTo(Entry bindable)
        {
            bindable.TextChanged += Bindable_TextChanged;
            base.OnAttachedTo(bindable);
        }

        protected override void OnDetachingFrom(Entry bindable)
        {
            bindable.TextChanged -= Bindable_TextChanged;
            base.OnDetachingFrom(bindable);
        }

        private void Bindable_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.NewTextValue))
            {
                var isValidDouble = double.TryParse(e.NewTextValue, out double value) && value >= 0;
                if (!isValidDouble)
                {
                    ((Entry)sender).Text = e.OldTextValue;
                }
            }
            else
            {
                ((Entry)sender).Text = null;
            }
        }
    }
}
