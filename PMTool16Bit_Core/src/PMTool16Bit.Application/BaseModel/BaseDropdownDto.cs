namespace PMTool16Bit.Services
{
    public class BaseDropdownDto : BaseDropdownDto<string>
    {
    }

    public class BaseDropdownDto<TKey>
    {
        public virtual TKey Id { get; set; }
        public string Text { get; set; }
    }
}