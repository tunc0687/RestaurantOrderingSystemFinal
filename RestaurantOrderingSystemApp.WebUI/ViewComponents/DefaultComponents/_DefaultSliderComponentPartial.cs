using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RestaurantOrderingSystemApp.BusinessLayer.Abstract;
using RestaurantOrderingSystemApp.WebUI.Dtos.FeatureDtos;

namespace RestaurantOrderingSystemApp.WebUI.ViewComponents.DefaultComponents
{
    public class _DefaultSliderComponentPartial(IFeatureService _featureService, IMapper _mapper) : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            var values = _mapper.Map<List<ResultFeatureDto>>(_featureService.TGetFeaturesByStatusTrue());
            if (values != null)
            {
                return View(values);
            }
            return View();
        }
    }

}
