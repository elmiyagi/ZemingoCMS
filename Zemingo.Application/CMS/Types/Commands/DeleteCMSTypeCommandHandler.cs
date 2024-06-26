using ZemingoCMS.Application.Abstractions.Commands;
using ZemingoCMS.Application.CMS.Types.Validation;
using ZemingoCMS.Application.Models;
using ZemingoCMS.Domain.Abstractions.Data;

namespace ZemingoCMS.Application.CMS.Types.Commands
{
    public record DeleteCMSTypeCommand(Guid Id) : ICommand<CommandResult>
    {
    }

    public interface IDeleteCMSTypeCommandHandler : ICommandHandler<DeleteCMSTypeCommand, CommandResult>
    {
    }
    public sealed class DeleteCMSTypeCommandHandler(IUnitOfWork unitOfWork) : IDeleteCMSTypeCommandHandler
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<CommandResult> Handle(DeleteCMSTypeCommand command)
        {
            var cmsItem = await _unitOfWork.CMSTypesRepository.GetById(command.Id);

            if (cmsItem == null)
                return new CommandResult(CMSTypeValidationConsts.NotFoundById(command.Id));

            _unitOfWork.CMSTypesRepository.Delete(cmsItem);
            await _unitOfWork.CommitAsync();

            return new CommandResult();
        }
    }
}
