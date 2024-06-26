using Microsoft.EntityFrameworkCore;
using ZemingoCMS.Application.Abstractions.Commands;
using ZemingoCMS.Application.CMS.Fields.Validation;
using ZemingoCMS.Application.CMS.Types.DTOs;
using ZemingoCMS.Application.CMS.Types.Mappings;
using ZemingoCMS.Application.CMS.Types.Validation;
using ZemingoCMS.Application.Models;
using ZemingoCMS.Application.Models.Validation;
using ZemingoCMS.Domain.Abstractions.Data;
using ZemingoCMS.Domain.CMS.Entities;

namespace ZemingoCMS.Application.CMS.Types.Commands
{
    public record UpdateCMSTypeCommand(Guid Id, UpdateCMSTypeDTO DTO) : ICommand<CommandResult>
    {
    }

    public interface IUpdateCMSTypeCommandHandler : ICommandHandler<UpdateCMSTypeCommand, CommandResult>
    {
    }
    public sealed class UpdateCMSTypeCommandHandler(IUnitOfWork unitOfWork) : IUpdateCMSTypeCommandHandler
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private CMSType? _cmsType;

        public async Task<CommandResult> Handle(UpdateCMSTypeCommand command)
        {
            var validationResult = await ValidateCommand(command);

            if (!validationResult.IsSuccess)
                return new CommandResult(validationResult.Errors);

            _cmsType.Update(command.DTO.Name);
            _unitOfWork.CMSTypesRepository.Update(_cmsType);
            await _unitOfWork.CommitAsync();

            return new ObjectCommandResult<CMSTypeDTO>(_cmsType.ToCMSTypeDTO());
        }

        private async Task<ValidationResult> ValidateCommand(UpdateCMSTypeCommand command)
        {
            if (string.IsNullOrEmpty(command.DTO.Name) || command.DTO.Name.Length > 255)
                return new ValidationResult(CMSTypeValidationConsts.NameLength());

            if (await _unitOfWork.CMSTypesRepository.Get().AnyAsync(x => x.Name == command.DTO.Name))
                return new ValidationResult(CMSTypeValidationConsts.NameAlreadyExists(command.DTO.Name));

            _cmsType = await _unitOfWork.CMSTypesRepository.GetById(command.Id);

            if (_cmsType == null)
                return new ValidationResult(CMSTypeValidationConsts.NotFoundById(command.Id));

            return new ValidationResult();
        }
    }
}
