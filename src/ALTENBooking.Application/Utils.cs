using ALTENBooking.Application.DTOs;
using ALTENBooking.Domain;
using ALTENBooking.Utils;

namespace ALTENBooking.Application
{
    internal static class Utils
    {
        internal static ResultDTO Execute(Func<ResultDTO> func)
        {
            ResultDTO result = new ResultDTO();
            try
            {
                result = func.Invoke();
            }
            catch (BookingException ex)
            {
                result.HasError = true;
                result.Message = StringMessages.ErrorOccurred + ": " + ex.Message;
            }
            catch (Exception ex)
            {
                result.HasError = true;
                result.Message = StringMessages.UnexpectedError;
            }

            return result;
        }
    }
}
