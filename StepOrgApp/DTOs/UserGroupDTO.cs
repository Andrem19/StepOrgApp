namespace StepOrg.DTOs
{
    public class UserGroupDTO
    {
        public string Name { get; set; }
        public string PictureUrl { get; set; }
        public int GroupId { get; set; }
        public bool IsSelected { get; set; } = false;
    }
}
