drop database hotelmanager1;

create database hotelmanager1;
use hotelmanager1;

create table Staff
(
	user_Id char(5) primary key,
    user_account varchar(100) not null unique,
    user_pass varchar(200) not null,
    user_name varchar(100) not null,
    user_address varchar(200),
    user_role int,
    is_active bit default 1
);
insert into Staff(user_Id,user_account, user_pass, user_name, user_role) values
	('NV001','kiennt1', '8e40de2a1f7d41b7eb05866ac0faeedd', 'Nguyen Trung Kien', 1),
    ('NV002','staff1', '8e40de2a1f7d41b7eb05866ac0faeedd', 'Nguyen Trung Kien', 1),
    ('NV003','staff2', '8e40de2a1f7d41b7eb05866ac0faeedd', 'Nguyen Trung Kien', 2),
    ('NV004','staff3', '8e40de2a1f7d41b7eb05866ac0faeedd', 'Nguyen Trung Kien', 2),
    ('NV005','staff4', '8e40de2a1f7d41b7eb05866ac0faeedd', 'Nguyen Trung Kien', 2);
    
-- pf11VTCAcademy

select * from Staff;

select * from Staff where  user_account='kiennt' and user_pass='8e40de2a1f7d41b7eb05866ac0faeedd' and is_active=1;

create table roomtype
(
	rtname varchar(10) primary key,
	quantity int not null,
	price int not null
);
insert into roomtype(rtname, quantity, price) values
('Standard',7,200000),
('Deluxe',6,300000),
('Suite',5,400000),
('Superio',3,500000);

create table room 
(
	roomid int primary key auto_increment,
	rtname varchar(10), 
	roomstatus bit,
    constraint fk_room_roomtype foreign key (rtname) references roomtype(rtname)
);

select roomid as MaPhong, checkin.customerid,  roomtype.rtname as LoaiPhong, TIMESTAMPDIFF(DAY,checkin.checkin, current_timestamp()) as SoNgayO, TIMESTAMPDIFF(DAY,checkin.checkin, current_timestamp()) * roomtype.price as GiaTien from room inner join roomtype  
on room.rtname = roomtype.rtname 
inner join checkin
on roomtype.rtname = checkin.rtname where room.roomstatus = '1' group by roomid order by roomid;
insert into room (rtname, roomstatus) values('Standard',1),
 ('Standard',1),('Standard',1),('Standard',0),('Standard',0),('Standard',0),('Standard',0),('Standard',0),('Standard',0),('Standard',0),
('Deluxe',1),('Deluxe',1),('Deluxe',1),('Deluxe',1),('Deluxe',0),('Deluxe',0),('Deluxe',0),('Deluxe',0),('Deluxe',0),('Deluxe',0),
('Suite',0),('Suite',0),('Suite',0),('Suite',0),('Suite',0),
('Superio',1),('Superio',0),('Superio',0),('Superio',0);


create table customer
(
	customerid int(5) primary key auto_increment,
    customername varchar(100) not null,
    address varchar(255),
    idcard char(9) unique not null,
    phonenumber varchar(12) not null,
    email varchar(55) unique,
    gender varchar(15)
);
insert into customer (customername, address, idcard, phonenumber, email, gender) values
('Võ Hoài Linh','Hồ Chí Minh','222555888','0909050505','hoailinh@yahoo.com','Nam'),
('Hồ Ngọc Hà','Hồ Chí Minh','222777511','01668135131','haho@yahoo.com','Nam'),
('Đàm Vĩnh Hưng','Hồ Chí Minh','222555782','0909050805','dvh@yahoo.com','Nam'),
('Hồ Quang Hiếu','Hồ Chí Minh','222444456','01668135181','hqh@yahoo.com','Nam'),
('Cao Thái Sơn','Hồ Chí Minh','229995753','0913050505','thaison@yahoo.com','Nam');

create table checkin
(
	checkin_id  char(5) primary key,
    staffid char(5) not null,
    customerid int(5) not null auto_increment,
    rtname varchar(10) not null,
    checkin datetime not null,
    checkout datetime not null,
    checkstatus bit,
    constraint fk_checkin_staff foreign key (staffid) references Staff(user_Id),
    constraint fk_checkin_customer foreign key(customerid) references customer(customerid),
    constraint fk_checkin_roomtype foreign key(rtname) references roomtype (rtname)
);
insert into checkin(checkin_id, staffid,rtname, checkin, checkout, checkstatus) values
('DP001','NV002','Superio','2020-01-23','2020-01-30',1),
('DP002','NV002','Standard','2021-08-10','2021-10-10',1),
('DP003','NV003','Deluxe','2021-05-18','2021-05-21',1),
('DP004','NV003','Deluxe','2021-06-20','2021-06-30',1),
('DP005','NV002','Suite','2021-03-24','2021-03-29',1);


create table checkin_room
(	
	checkin_id char(5),
    room_id int auto_increment,
    constraint fk_checkin_room_checkin foreign key (checkin_id) references checkin(checkin_id),
    constraint fk_ccheckin_room_room foreign key (room_id) references room (roomid),
    primary key(checkin_id,room_id)
);

insert into checkin_room (checkin_id) values 
('DP001'),
('DP001'),
('DP002'),
('DP002'),
('DP003'),
('DP003'),
('DP004'),
('DP004');

create table bill
(
	bill_id char(5) primary key,
    checkin_id char(5),
    date_create datetime,
    total_money int,
    constraint fk_bill_checkin foreign key(checkin_id) references checkin(checkin_id)
);

insert into bill(bill_id, checkin_id,date_create,total_money) values('HD001','DP005','2021-03-29',2400000);