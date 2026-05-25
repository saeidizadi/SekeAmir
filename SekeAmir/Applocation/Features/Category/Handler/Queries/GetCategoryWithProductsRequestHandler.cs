using Application.Contracts.Shop;
using Application.Features.Category.Request.Queries;
using MediatR;
using PersianAssistant.Models;

namespace Application.Features.Category.Handler.Queries;

public class GetCategoryWithProductsRequestHandler(ICategory categoryRepository) : IRequestHandler<GetCategoryWithProductsRequest, ServiceMessage>
{
    public async Task<ServiceMessage> Handle(GetCategoryWithProductsRequest request, CancellationToken cancellationToken)
    {
        var categories = await categoryRepository.GetAll();
        throw new NotImplementedException();
    }
}