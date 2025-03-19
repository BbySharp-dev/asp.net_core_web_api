[FromBody]:  
[FromBody] là một thuộc tính trong ASP.NET Core dùng để chỉ định rằng dữ liệu của tham số addRegionRequestDto sẽ được lấy từ phần thân của yêu cầu HTTP (HTTP request body). Điều này thường được sử dụng trong các yêu cầu POST hoặc PUT khi dữ liệu được gửi dưới dạng JSON hoặc XML.
Chuyển đổi DTO đầu vào thành domain model:  
DTO (Data Transfer Object): Là một đối tượng được sử dụng để truyền dữ liệu giữa các lớp hoặc các tầng của ứng dụng. DTO thường chỉ chứa dữ liệu và không có logic nghiệp vụ.
Domain Model: Là các đối tượng đại diện cho các thực thể trong ứng dụng và chứa logic nghiệp vụ. Domain model thường được ánh xạ trực tiếp tới các bảng trong cơ sở dữ liệu.
Lý do chuyển đổi: Chúng ta chuyển đổi DTO đầu vào thành domain model để có thể làm việc với các đối tượng domain trong logic nghiệp vụ và lưu trữ chúng vào cơ sở dữ liệu. Domain model chứa các thuộc tính và logic cần thiết để tương tác với cơ sở dữ liệu.
Chuyển domain model thành DTO:
Sau khi lưu trữ domain model vào cơ sở dữ liệu, chúng ta chuyển đổi nó trở lại thành DTO để trả về cho client. DTO giúp tách biệt dữ liệu trả về từ logic nghiệp vụ và chỉ chứa các thông tin cần thiết cho client.

Giải thích hàm POST:
Nhận dữ liệu từ client: Hàm CreateRegion nhận một đối tượng AddRegionRequestDto từ phần thân của yêu cầu HTTP nhờ thuộc tính [FromBody].
Chuyển đổi DTO thành domain model: Tạo một đối tượng Region (domain model) từ AddRegionRequestDto.
Lưu domain model vào cơ sở dữ liệu: Thêm đối tượng Region vào DbContext và gọi SaveChanges để lưu thay đổi vào cơ sở dữ liệu.
Chuyển đổi domain model thành DTO: Tạo một đối tượng RegionDto từ Region để trả về cho client.
Trả về kết quả: Trả về đối tượng RegionDto với trạng thái HTTP 201 Created, cùng với URL để truy cập đối tượng vừa tạo.