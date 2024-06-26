using ZemingoCMS.Application.Abstractions.Commands;
using ZemingoCMS.Application.CMS.Fields.Validation;
using ZemingoCMS.Application.CMS.Types.Validation;
using ZemingoCMS.Application.Models;
using ZemingoCMS.Domain.Abstractions.Data;
using ZemingoCMS.Domain.CMS.Entities;

namespace ZemingoCMS.Application.CMS.Fields.Commands
{
    public record DeleteCMSFieldCommand(Guid Id) : ICommand<CommandResult>
    {
    }

    public interface IDeleteCMSFieldCommandHandler : ICommandHandler<DeleteCMSFieldCommand, CommandResult>
    {
    }
    public sealed class DeleteCMSFieldCommandHandler(IUnitOfWork unitOfWork) : IDeleteCMSFieldCommandHandler
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<CommandResult> Handle(DeleteCMSFieldCommand command)
        {
            var cmsField = await _unitOfWork.CMSFieldsRepository.GetById(command.Id);

            if (cmsField == null)
                return new CommandResult(CMSFieldValidationConsts.NotFoundById(command.Id));

            _unitOfWork.CMSFieldsRepository.Delete(cmsField);
            await _unitOfWork.CommitAsync();

            return new CommandResult();
        }
    }
}
