namespace Shared.Domain.Common
{
    public class ApiMessage
    {
        public const string UPLOAD_SUCCESS = "Tải file/tệp thành công";
        public const string UPLOAD_FAILED = "Tải file/tệp thất bại";


        public const string SAVE_PROPERTYCART_SUCCESS = "Lưu giỏ hàng thành công";
        public const string DELETE_PROPERTYCART_SUCCESS = "Bỏ lưu BĐS thành công";

        public const string SAVE_CUSTOMERCART_SUCCESS = "Lưu khách hàng thành công";
        public const string DELETE_CUSTOMERCART_SUCCESS = "Bỏ lưu khách hàng thành công";

        public const string GETALL_SUCCESS = "Lấy danh sách dữ liệu thành công";
        public const string GET_SUCCESS = "Lấy dữ liệu thành công";
        public const string CREATE_SUCCESS = "Thêm dữ liệu thành công";
        public const string UPDATE_SUCCESS = "Cập nhật dữ liệu thành công";
        public const string DELETE_SUCCESS = "Xóa dữ liệu thành công";

        public const string CREATE_FAILED = "Thêm dữ liệu thất bại";
        public const string UPDATE_FAILED = "Cập nhật dữ liệu thất bại";
        public const string DELETE_FAILED = "Xóa dữ liệu thất bại";

        public const string IMPORT_WAREHOUSE_SUCCESS = "Nhập kho thành công";
        public const string IMPORT_WAREHOUSE_FAILED = "Nhập kho thất bại";
        public const string EXPORT_WAREHOUSE_SUCCESS = "Xuất kho thành công";
        public const string EXPORT_WAREHOUSE_FAILED = "Xuất kho thất bại";

        public const string SENDMAIL_SUCCESS = "Gửi Email thành công!";
        public const string SENDMAIL_FAILED = "Gửi Email thất bại";

        public const string CANNOT_ACCESS = "Không có quyền truy cập dữ liệu này!";
        public const string UNAUTHORIZED = "Hết hạn đăng nhập, hãy đăng nhập lại!";
        public const string FORBIDDEN = "Không có quyền thực hiện";
        public const string EXPIRE_TOKEN = "Phiên đăng nhập đã hết hiệu lực";
        //public const string AUTHENTICATION_ERROR = "Lỗi xác thực";
        public const string EXIST_CODE = "Mã đã tồn tại, vui lòng kiểm tra lại!";
        public const string NOT_FOUND = "Không tìm thấy dữ liệu";
        public const string NO_DATA = "Không có dữ liệu";

        public const string LOGIN_SUCCESS = "Đăng nhập thành công";
        public const string LOGIN_BLOCK = "Tài khoản bị vô hiệu/khóa, vui lòng liên hệ Quản lý";
        public const string LOGIN_WRONG_USERNAME = "Không tìm thấy thông tin tài khoản!";
        public const string LOGIN_WRONG_PASSWORD = "Mật khẩu không đúng!";
        public const string LOGOUT_SUCCESS = "Đăng xuất thành công";

        public const string ERROR_MSG = "Có lỗi xảy ra, vui lòng thử lại sau!";
        public const string REQUIRE_MSG = "Thiếu thông tin";
        public const string STATUS_ERROR = "Trạng thái không hợp lệ!";
    }
}
