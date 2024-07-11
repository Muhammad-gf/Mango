using AutoMapper;
using Mango.Services.CouponAPI.Data;
using Mango.Services.CouponAPI.Models;
using Mango.Services.CouponAPI.Models.Dto;
using Microsoft.AspNetCore.Mvc;

namespace Mango.Services.CouponAPI.Controllers
{
    [Route("api/coupon")]
    [ApiController]
    public class CouponAPIController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;
        private readonly ResponseDto _response;
        private readonly IMapper _mapper;

        public CouponAPIController(AppDbContext dbContext, IMapper _mapper)
        {
            this._appDbContext = dbContext;
            this._response = new ResponseDto();
            this._mapper = _mapper;
        }

        [HttpGet]
        public ResponseDto Get()
        {
            try
            {
                var coupons = _appDbContext.Coupons.ToList();
                _response.Result = _mapper.Map<IEnumerable<CouponDto>>(coupons);
            }
            catch (Exception ex) {
                _response.Message = ex.Message;
                _response.IsSuccess = false;   
            }

            return _response;
        }

        [HttpGet]
        [Route("{id}")]
        public ResponseDto Get(int id)
        {
            try
            {
                var coupon = _appDbContext.Coupons.First(x => x.CouponId == id);
                _response.Result = _mapper.Map<CouponDto>(coupon);
            }
            catch (Exception ex)
            {
                _response.Message = ex.Message;
                _response.IsSuccess = false;
            }

            return _response;
        }

        [HttpGet]
        [Route("GetByCode/{code}")]
        public ResponseDto GetByCode(string code)
        {
            try
            {
                var coupon = _appDbContext.Coupons.First(x => x.CouponCode.ToLower() == code.ToLower());
                _response.Result = _mapper.Map<CouponDto>(coupon);
            }
            catch (Exception ex)
            {
                _response.Message = ex.Message;
                _response.IsSuccess = false;
            }

            return _response;
        }

        [HttpPost]
        public ResponseDto Post([FromBody] CouponDto coupon)
        {
            try
            {
                var couponObj = _mapper.Map<Coupon>(coupon);
                _appDbContext.Coupons.Add(couponObj);
                _appDbContext.SaveChanges();

                _response.Result = _mapper.Map<CouponDto>(couponObj);
            }
            catch (Exception ex)
            {
                _response.Message = ex.Message;
                _response.IsSuccess = false;
            }

            return _response;
        }

        [HttpPut]
        public ResponseDto Put([FromBody] CouponDto coupon)
        {
            try
            {
                var couponObj = _mapper.Map<Coupon>(coupon);
                _appDbContext.Coupons.Update(couponObj);
                _appDbContext.SaveChanges();

                _response.Result = _mapper.Map<CouponDto>(couponObj);
            }
            catch (Exception ex)
            {
                _response.Message = ex.Message;
                _response.IsSuccess = false;
            }

            return _response;
        }

        [HttpDelete]
        [Route("{id}")]
        public ResponseDto Delete(int id)
        {
            try
            {
                var coupon = _appDbContext.Coupons.First(x => x.CouponId == id);
                _appDbContext.Coupons.Remove(coupon);
                _appDbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                _response.Message = ex.Message;
                _response.IsSuccess = false;
            }

            return _response;
        }
    }
}
