using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using BugTicketingSystem.BL.Validators.Bug;
using BugTicketingSystem.DAL;
using BugTicketingSystem.DAL.Models;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BugTicketingSystem.BL.Managers.Attachements
{
    public class AttachementManager : IAttachementManager
    {
        private readonly IUnitOfWork _unitOfWork;
        public AttachementManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        //----------------------------------------------------AddAttachement----------------------------------------------------------------------------------------------//
        public async Task<GeneralResult> AddAttachementAsync(Guid bug_id, FileUploadRequest fileRequest)
        {
            bool bug = await IsBugIdFound(bug_id);
            if (!bug)
            {
                return GeneralResult.Fail(GenerateArrayOfErrors("no bug with this id"));

            }
            var file = fileRequest.File;
            #region Validation

            if (file.Length == 0)
            {
                return GeneralResult.Fail(GenerateArrayOfErrors("you must uplad file"));
            }
            if (file.Length > 5 * 1024 * 1024)
            {

                return GeneralResult.Fail(GenerateArrayOfErrors("File is too large"));

            }
            var exteenstion = Path.GetExtension(file.FileName).ToLowerInvariant();
            #endregion
            var filePath = Path.Combine(
               Directory.GetCurrentDirectory(),
               "Images", $"{Guid.NewGuid()}{exteenstion}"
               );
            using var stream = new FileStream(filePath, FileMode.Create);
            await file.CopyToAsync(stream);
            string fileURL = $"/api/my-static-files/{Path.GetFileName(filePath)}";

            Attachment attachementToBeAdded = new Attachment
            {
                FileName = file.FileName,
                FilePath = filePath,
                FileURL = fileURL,
                Bug_Id = bug_id,
                CreatedAt = DateTime.Now,
            };

            _unitOfWork._attachementRepo.Add(attachementToBeAdded);
            var saveResult = await _unitOfWork.SaveChangesAsync();

            if (saveResult > 0)
            {
                return GeneralResult.Ok();
            }
            else
            {

                return GeneralResult.Fail(GenerateArrayOfErrors("unable to add this Attachement"));

            }
        }

        //----------------------------------------------------Get Bug With Atatchement----------------------------------------------------------------------------------------------//
        public async Task<GeneralResult<BugWithAttachementReadDTO>> GetBugByIdWithAttachementJoinAsync(Guid Bug_id)
        {
            var detailedBug = await _unitOfWork._bugRepo.GetBugWithHisAttachmentAsync(Bug_id);
            if (detailedBug == null)
            {

                return GeneralResult<BugWithAttachementReadDTO>.Fail(GenerateArrayOfErrors("their is no bug with this id"));
            }
            else
            {

                var bugReadWithDetailesDTO = new BugWithAttachementReadDTO
                {

                    ProjectName = detailedBug.Project.ProjectName,
                    CreatedAt = detailedBug.CreatedAt,
                    UpdatedAt = detailedBug.UpdatedAt,
                    BugId = detailedBug.BugId,
                    BugTitle = detailedBug.Title,
                    Attachements = detailedBug.Attachments.Select(us => new AttachementDetailes { AttachementName = us.FileName, AttachementURL = us.FileURL, AttachementId = us.AttachementId }).ToList(),


                };
                return GeneralResult<BugWithAttachementReadDTO>.Ok(bugReadWithDetailesDTO);
            }
        }
        //----------------------------------------------------Remove an attachment from a bug----------------------------------------------------------------------------------------------//
        public async Task<GeneralResult> DeleteAttachementFromBug(Guid attachementId)
        {
            var attachement = await _unitOfWork._attachementRepo.GetByIdAsync(attachementId);
            if (attachement == null)
            {
                // this means tha the user isn't assigned with this bug before 

                return GeneralResult.Fail(GenerateArrayOfErrors("this bug is not found"));

            }
            _unitOfWork._attachementRepo.Delete(attachement);
            var saveResult = await _unitOfWork.SaveChangesAsync();

            if (saveResult > 0)
            {
                return GeneralResult.Ok();
            }
            else
            {
                return GeneralResult.Fail(GenerateArrayOfErrors("Unable to delete this attachement"));
            }

        }
        //---------------------------------------------------------------------------------------------------------------------------------//

        private ResultError[] GenerateArrayOfErrors(string v)
        {
            return new ResultError[]
 {
    new ResultError
    {
        Code = null ,
        Message = v
    }
 };
        }

        //---------------------------------------------------------------------------------------------------------------------------------//
        private async Task<bool> IsBugIdFound(Guid bug_id)
        {
            return await _unitOfWork._bugRepo.GetByIdAsync(bug_id) != null;
        }
        //---------------------------------------------------------------------------------------------------------------------------------//



    }
}
