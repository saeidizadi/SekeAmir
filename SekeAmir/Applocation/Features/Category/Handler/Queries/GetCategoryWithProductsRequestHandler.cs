using Application.Contracts.Shop;
using Application.Features.Category.Request.Queries;
using MediatR;
using PersianAssistant.Models;

namespace Application.Features.Category.Handler.Queries;

public class GetCategoryWithProductsRequestHandler(ICategory categoryRepository, IProduct product) : IRequestHandler<GetCategoryWithProductsRequest, ServiceMessage>
{
    public async Task<ServiceMessage> Handle(GetCategoryWithProductsRequest request, CancellationToken cancellationToken)
    {
        var categories = await categoryRepository.GetAll();
        foreach (var category in categories)
        {
            //var productsWithPrice =
        }
        throw new NotImplementedException();
    }
}