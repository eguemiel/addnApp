namespace AddnApp.Events
{
    public delegate void OnBackPressDelegate(EventBackPressedArgs e);

    public class EventBackPressedArgs
    {
        public bool Cancel { get; set; }
    }
}