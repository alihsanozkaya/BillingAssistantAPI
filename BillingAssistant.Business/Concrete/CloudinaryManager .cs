﻿using BillingAssistant.Business.Abstract;
using BillingAssistant.Core.Utilities.Cloudinary;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Npgsql.BackendMessages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillingAssistant.Business.Concrete
{
    public class CloudinaryManager : ICloudinaryService
    {
        private readonly Cloudinary _cloudinary;

        public CloudinaryManager(IOptions<CloudinarySettings> cloudinaryConfig)
        {
            var account = new Account(
                cloudinaryConfig.Value.CloudName,
                cloudinaryConfig.Value.ApiKey,
                cloudinaryConfig.Value.ApiSecret);

            _cloudinary = new Cloudinary(account);
        }
        public async Task<string> UploadImageAsync(string imagePath)
        {
            var uploadParams = new ImageUploadParams
            {
                File = new FileDescription(imagePath)
            };

            var uploadResult = await _cloudinary.UploadAsync(uploadParams);

            return uploadResult.SecureUri.ToString();
        }
        public async Task<UploadResult> UploadOrderImageAsync(IFormFile file)
        {
            if (file.Length <= 0)
            {
                return null;
            }
            using (var stream = file.OpenReadStream())
            {
                var uploadParams = new ImageUploadParams
                {
                    File = new FileDescription(file.FileName, stream),
                    Transformation = new Transformation().Crop("fill").Width(496).Height(711)
                };
                var uploadResult = await _cloudinary.UploadAsync(uploadParams);
                return uploadResult;
            }
        }
    }
}