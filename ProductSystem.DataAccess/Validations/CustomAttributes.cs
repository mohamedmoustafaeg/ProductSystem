using System;
using System.ComponentModel.DataAnnotations;
using System.IO;

public class ValidateImageByteArrayAttribute : ValidationAttribute
{
    private readonly int _maxFileSize;
    private readonly string[] _allowedFormats = { "image/jpeg", "image/png" };

    public ValidateImageByteArrayAttribute(int maxFileSize)
    {
        _maxFileSize = maxFileSize;
    }

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value is byte[] imageData)
        {
            // Check file size
            if (imageData.Length > _maxFileSize)
            {
                return new ValidationResult($"File size cannot exceed {_maxFileSize / (1024 * 1024)} MB.");
            }

            // Check file type by looking at the header bytes
            if (!IsImageFormatValid(imageData))
            {
                return new ValidationResult("Only JPG, JPEG, and PNG formats are allowed.");
            }
        }
        return ValidationResult.Success;
    }

    private bool IsImageFormatValid(byte[] imageData)
    {
        // Check for JPG or JPEG header (0xFFD8FFE0 or 0xFFD8FFE1)
        if (imageData.Length > 4 &&
           ((imageData[0] == 0xFF && imageData[1] == 0xD8 && imageData[2] == 0xFF && (imageData[3] == 0xE0 || imageData[3] == 0xE1))))
        {
            return true;
        }

        // Check for PNG header (0x89504E47)
        if (imageData.Length > 4 &&
           (imageData[0] == 0x89 && imageData[1] == 0x50 && imageData[2] == 0x4E && imageData[3] == 0x47))
        {
            return true;
        }

        return false;
    }
}
