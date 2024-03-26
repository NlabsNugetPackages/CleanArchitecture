using CleanArchitecture.Application.Utilities;
using MediatR;

namespace CleanArchitecture.Application.Features.Brand.Create;
public sealed record CreateBrandCommand(
    string Name,
    string Description
) : IRequest<Result<string>>;


internal sealed class CreateBrandCommandHandler : IRequestHandler<CreateBrandCommand, Result<string>>
{
    public Task<Result<string>> Handle(CreateBrandCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}