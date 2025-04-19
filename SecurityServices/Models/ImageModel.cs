namespace SecurityServices.Models
{
    public class ImageModel
    {
        public int Id { get; set; }  // Image identifier
        public string FileName { get; set; }  // Original file name
        public string ContentType { get; set; }  // MIME type (image/jpg, image/png, etc.)
        public byte[] Data { get; set; }  // Image data as a byte array
    }

}
