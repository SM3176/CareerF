using System.IO;

namespace CareerFIZ.Common
{
    public class DeleteImage
    {
        public static bool DeleteImageFile(string imagePath)
        {
            try
            {
                if (File.Exists(imagePath))
                {
                    File.Delete(imagePath);
                    return true;
                }
            }
            catch (Exception ex)
            {
                //Example
                Console.WriteLine($"An error occurred while deleting skill: {ex.Message}");
            }
            return false;
        }
    }
}