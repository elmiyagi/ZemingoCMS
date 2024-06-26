using Microsoft.EntityFrameworkCore;
using ZemingoCMS.Application.Abstractions.Commands;
using ZemingoCMS.Application.CMS.Fields.DTOs;
using ZemingoCMS.Application.CMS.Fields.Mappings;
using ZemingoCMS.Application.CMS.Fields.Validation;
using ZemingoCMS.Application.CMS.Types.Validation;
using ZemingoCMS.Application.Models;
using ZemingoCMS.Application.Models.Validation;
using ZemingoCMS.Domain.Abstractions.Data;
using ZemingoCMS.Domain.CMS.Entities;

namespace ZemingoCMS.Application.CMS.Fields.Commands
{
    public record UpdateCMSFieldCommand(Guid Id, UpdateCMSFieldDTO DTO) : ICommand<CommandResult>
    {
    }

    public interface IUpdateCMSFieldCommandHandler : ICommandHandler<UpdateCMSFieldCommand, CommandResult>
    {
    }
    public sealed class UpdateCMSFieldCommandHandler(IUnitOfWork unitOfWork) : IUpdateCMSFieldCommandHandler
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private CMSField? _cmsField;

        public async Task<CommandResult> Handle(UpdateCMSFieldCommand command)
        {
            var validationResult = await ValidateCommand(command);

            if (!validationResult.IsSuccess)
                return new CommandResult(validationResult.Errors);

            _cmsField.Update(command.DTO.Name, command.DTO.FieldType);
            _unitOfWork.CMSFieldsRepository.Update(_cmsField);
            await _unitOfWork.CommitAsync();

            return new ObjectCommandResult<CMSFieldDTO>(_cmsField.ToCMSFieldDTO());
        }

        private async Task<ValidationResult> ValidateCommand(UpdateCMSFieldCommand command)
        {
            if (string.IsNullOrEmpty(command.DTO.Name) || command.DTO.Name.Length > 255)
                return new ValidationResult(CMSFieldValidationConsts.NameLength());

            if (await _unitOfWork.CMSFieldsRepository.Get()
                .AnyAsync(x => x.Name == command.DTO.Name && x.Type.Id == command.DTO.CmsTypeId))
                return new ValidationResult(CMSFieldValidationConsts.NameAlreadyExists(command.DTO.Name));

            _cmsField = await _unitOfWork.CMSFieldsRepository.GetById(command.Id);
            if (_cmsField == null)
                return new ValidationResult(CMSFieldValidationConsts.NotFoundById(command.Id));

            var cmsType = await _unitOfWork.CMSTypesRepository.GetById(command.Id);
            if (cmsType == null)
                return new ValidationResult(CMSTypeValidationConsts.NotFoundById(command.Id));

            return new ValidationResult();
        }
    }
}
