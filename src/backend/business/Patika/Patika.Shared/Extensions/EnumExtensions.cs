using Patika.Shared.Enums;

namespace Patika.Shared.Extensions
{
    public static class EnumExtensions
    {
        public static FileSizes GetImageFileSize(this FileTypes fileType)
        {
            return fileType switch
            {
                FileTypes.Profile => FileSizes.SmallAndMediumAndLarge,
                FileTypes.Message => FileSizes.Large,
                FileTypes.SuggestionCategories => FileSizes.Large, 
                FileTypes.Certificates => FileSizes.MediumAndLarge, 
                FileTypes.ReferenceJobs => FileSizes.SmallAndMediumAndLarge, 
                _ => FileSizes.Large,
            };
        }

        public static bool IsSmallWanted(this FileSizes fileSize) => fileSize switch
        {
            FileSizes.Small => true,
            FileSizes.SmallAndLarge => true,
            FileSizes.SmallAndMedium => true,
            FileSizes.SmallAndMediumAndLarge => true,
            _ => false
        };

        public static bool IsMediumWanted(this FileSizes fileSize) => fileSize switch
        {
            FileSizes.Medium => true,
            FileSizes.MediumAndLarge => true,
            FileSizes.SmallAndMedium => true,
            FileSizes.SmallAndMediumAndLarge => true,
            _ => false
        };

    }
}
