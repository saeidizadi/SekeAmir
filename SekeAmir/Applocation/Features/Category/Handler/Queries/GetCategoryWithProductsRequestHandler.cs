using Application.Contracts.Shop;
using Application.Features.Category.Request.Queries;
using Domain;
using MediatR;
using PersianAssistant.Extensions;
using PersianAssistant.Models;

namespace Application.Features.Category.Handler.Queries;

public class GetCategoryWithProductsRequestHandler(IProductPrice productPriceRepository) : IRequestHandler<GetCategoryWithProductsRequest, ServiceMessage>
{
    public async Task<ServiceMessage> Handle(GetCategoryWithProductsRequest request, CancellationToken cancellationToken)
    {
        var Product = await productPriceRepository.showAllPrices(InputType.api);
        return ResponseManager.CustomResponse(1, "ok", Product);
    }
}