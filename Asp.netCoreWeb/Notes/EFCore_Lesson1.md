# Buổi Học Đầu Tiên: EF Core và Các Phương Thức Quan Trọng (macOS)

## Tổng Quan EF Core
- **EF Core**: Entity Framework Core là một framework mã hóa đầu tiên (ORM) cho .NET, giúp lập trình viên làm việc với cơ sở dữ liệu trên macOS một cách hiệu quả bằng cách ánh xạ đối tượng trong mã nguồn với bảng trong cơ sở dữ liệu.

## Các Lệnh Quan Trọng (macOS)
- **Cài đặt công cụ EF Core**:
    - Lệnh: `dotnet tool install --global dotnet-ef`
    - Mô tả: Cài đặt công cụ EF Core toàn cục trên macOS.
- **Add-Migration InitialCreate**:
    - Lệnh: `dotnet ef migrations add InitialCreate`
    - Mô tả: Tạo migration mới với tên "InitialCreate" dựa trên mô hình dữ liệu.
- **Update-Database**:
    - Lệnh: `dotnet ef database update`
    - Mô tả: Áp dụng migration vào cơ sở dữ liệu.
- **Kiểm tra phiên bản**:
    - Lệnh: `dotnet ef --version`
    - Mô tả: Xác nhận phiên bản công cụ EF Core đã cài đặt.

## So Sánh Các Phương Thức EF Core

### 1. FindAsync
- **Khi nào dùng?**: Sử dụng khi cần tìm một bản ghi dựa trên khóa chính (Primary Key) một cách nhanh chóng, đặc biệt trong API cần hiệu suất cao trên macOS.
- **Dấu hiệu nhớ**: Phù hợp khi có ID duy nhất (như GUID) và chắc chắn là khóa chính.
- **Lưu ý**: Chỉ dùng khi không cần điều kiện phức tạp, chỉ dựa vào khóa chính.

### 2. FirstAsync
- **Khi nào dùng?**: Áp dụng khi chắc chắn có ít nhất một bản ghi thỏa mãn điều kiện và muốn nhận lỗi nếu không tìm thấy (phù hợp khi dữ liệu phải luôn tồn tại).
- **Dấu hiệu nhớ**: Dùng khi không chấp nhận giá trị null, cần kiểm tra dữ liệu bắt buộc.
- **Lưu ý**: Có thể ném lỗi InvalidOperationException nếu không có bản ghi, cần dùng cẩn thận.

### 3. FirstOrDefaultAsync
- **Khi nào dùng?**: Sử dụng khi cần phần tử đầu tiên thỏa mãn điều kiện, nhưng chấp nhận trường hợp không có bản ghi (trả về null). Thích hợp cho API như GetRegionById.
- **Dấu hiệu nhớ**: Dùng khi linh hoạt, an toàn, và cần bất đồng bộ trên macOS.
- **Lưu ý**: Tối ưu cho các truy vấn đơn lẻ, không yêu cầu toàn bộ danh sách dữ liệu.

### 4. IgnoreQueryFilters
- **Khi nào dùng?**: Áp dụng khi cần bỏ qua các bộ lọc toàn cục (như soft delete) để truy cập tất cả dữ liệu, kể cả những bản ghi bị ẩn.
- **Dấu hiệu nhớ**: Dùng khi cần xem hoặc khôi phục dữ liệu bị ẩn, ví dụ như bản ghi đã xóa mềm.
- **Lưu ý**: Kết hợp với các phương thức khác để lấy dữ liệu sau khi bỏ lọc.

### 5. FromSqlInterpolated
- **Khi nào dùng?**: Sử dụng khi cần chạy truy vấn SQL thô với tham số nội suy, phù hợp cho các truy vấn phức tạp mà LINQ không thể xử lý.
- **Dấu hiệu nhớ**: Áp dụng khi cần viết SQL trực tiếp, nhưng phải cẩn thận để tránh rủi ro bảo mật.
- **Lưu ý**: Sử dụng tham số an toàn để ngăn chặn các vấn đề như SQL Injection.

## Mẹo Nhớ Nhanh
- **Khóa chính**: FindAsync (nhanh, ID duy nhất).
- **Bắt buộc có**: FirstAsync (ép có kết quả).
- **Có thể trống**: FirstOrDefaultAsync (an toàn, linh hoạt).
- **Bỏ lọc**: IgnoreQueryFilters (xem dữ liệu ẩn).
- **SQL thô**: FromSqlInterpolated (phức tạp, cần cẩn thận).

## Ghi Chú Thực Tế
- **Trong RegionsController**: Nên dùng FirstOrDefaultAsync cho GetRegionById vì chỉ cần một vùng, có thể không tồn tại, và tận dụng tính bất đồng bộ trên macOS.
- **Kiểm tra**: Thử nghiệm từng phương thức với công cụ như Datagrip trên macOS để hiểu rõ sự khác biệt (ví dụ: FindAsync với ID, IgnoreQueryFilters với dữ liệu xóa mềm).