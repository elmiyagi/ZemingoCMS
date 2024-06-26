using Microsoft.EntityFrameworkCore;
using ZemingoCMS.Application.Abstractions.Commands;
using ZemingoCMS.Application.CMS.Types.DTOs;
using ZemingoCMS.Application.CMS.Types.Mappings;
using ZemingoCMS.Application.CMS.Types.Validation;
using ZemingoCMS.Application.Models;
using ZemingoCMS.Application.Models.Validation;
using ZemingoCMS.Domain.Abstractions.Data;
using ZemingoCMS.Domain.CMS.Entities;

namespace ZemingoCMS.Application.CMS.Types.Commands
{
    public record AddCMSTypeCommand(AddCMSTypeDTO DTO) : ICommand<CommandResult>
    {
    }

    public interface IAddCMSTypeCommandHandler : ICommandHandler<AddCMSTypeCommand, CommandResult>
    {
    }
    public sealed class AddCMSTypeCommandHandler(IUnitOfWork unitOfWork) : IAddCMSTypeCommandHandler
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<CommandResult> Handle(AddCMSTypeCommand command)
        {
            var validationResult = await ValidateCommand(command);

            if (!validationResult.IsSuccess)
                return new CommandResult(validationResult.Errors);

            var cmsType = CMSType.Create(Guid.NewGuid(), command.DTO.Name);
            _unitOfWork.CMSTypesRepository.Add(cmsType);
            await _unitOfWork.CommitAsync();

            return new ObjectCommandResult<CMSTypeDTO>(cmsType.ToCMSTypeDTO());
        }

        private async Task<ValidationResult> ValidateCommand(AddCMSTypeCommand command)
        {
            if (string.IsNullOrEmpty(command.DTO.Name) || command.DTO.Name.Length > 255)
                return new ValidationResult(CMSTypeValidationConsts.NameLength());

            if (await _unitOfWork.CMSTypesRepository.Get().AnyAsync(x => x.Name == command.DTO.Name))
                return new ValidationResult(CMSTypeValidationConsts.NameAlreadyExists(command.DTO.Name));

            return new ValidationResult();
        }
    }
}
