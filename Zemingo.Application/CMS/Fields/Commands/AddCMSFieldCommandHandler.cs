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
    public record AddCMSFieldCommand(AddCMSFieldDTO DTO) : ICommand<CommandResult>
    {
    }

    public interface IAddCMSFieldCommandHandler : ICommandHandler<AddCMSFieldCommand, CommandResult>
    {
    }
    public sealed class AddCMSFieldCommandHandler(IUnitOfWork unitOfWork) : IAddCMSFieldCommandHandler
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private CMSType? _cmsType;

        public async Task<CommandResult> Handle(AddCMSFieldCommand command)
        {
            var validationResult = await ValidateCommand(command);

            if (!validationResult.IsSuccess)
                return new CommandResult(validationResult.Errors);

            var cmsField = CMSField.Create(Guid.NewGuid(), command.DTO.Name, command.DTO.FieldType, _cmsType);
            _unitOfWork.CMSFieldsRepository.Add(cmsField);
            await _unitOfWork.CommitAsync();

            return new ObjectCommandResult<CMSFieldDTO>(cmsField.ToCMSFieldDTO());
        }

        private async Task<ValidationResult> ValidateCommand(AddCMSFieldCommand command)
        {
            if (string.IsNullOrEmpty(command.DTO.Name) || command.DTO.Name.Length > 255)
                return new ValidationResult(CMSFieldValidationConsts.NameLength());

            if (_unitOfWork.CMSFieldsRepository.Get()
                    .Any(x => x.Name == command.DTO.Name && x.Type.Id == command.DTO.CMSTypeId))
                return new ValidationResult(CMSFieldValidationConsts.NameAlreadyExists(command.DTO.Name));

            var cmsType = await _unitOfWork.CMSTypesRepository.GetById(command.DTO.CMSTypeId);

            if (cmsType == null)
                return new ValidationResult(CMSTypeValidationConsts.NotFoundById(command.DTO.CMSTypeId));

            _cmsType = cmsType;

            return new ValidationResult();
        }
    }
}
