using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Skipper.Models.DTOs.Incomig;

namespace Skipper.Controllers
{
    public class TestController : ControllerBase
    {
        private readonly UnitOfWork _unitOfWork;

        public TestController(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

    }
}
