using Mango.Web.Models;

namespace Mango.Web.Services.IServices
{
    public interface ICouponService
    {
        Task<ResponseDto?> GetAllCouponsAsync();
        
        Task<ResponseDto?> GetCouponAsync(string couponCode);

        Task<ResponseDto?> GetCouponByIdAsync(int id);
        
        Task<ResponseDto?> CreateCouponAsync(CouponDto coupon);
        
        Task<ResponseDto?> UpdateCouponAsync(CouponDto coupon);
        
        Task<ResponseDto?> DeleteCouponAsync(int id);
    }
}
