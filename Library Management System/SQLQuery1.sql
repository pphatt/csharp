create table Book
(
    BookIDs             int primary key IDENTITY (1,1),
    BookName            nvarchar(50)                       not null,
    BookAuthor          nvarchar(50),
    BookCategory        nvarchar(50),
    BookAmountAvailable int check (0 <= BookAmountAvailable and BookAmountAvailable <= 100),
    BookAmountBorrowed  int check (BookAmountBorrowed >= 0),
    Date                datetime,
    PublishDate         date,
    State               int check (State = 0 or State = 1) not null,
    LIDs                int,
)

create table Customer
(
    CustomerIDs         int primary key IDENTITY (1,1),
    CustomerName        nvarchar(50)                       not null,
    CustomerAge         int check (18 <= CustomerAge and CustomerAge <= 80),
    CustomerSex         nvarchar(10) check (CustomerSex = 'Male' or CustomerSex = 'Female'),
    CustomerPhoneNumber nvarchar(12) check (10 <= LEN(CustomerPhoneNumber) and LEN(CustomerPhoneNumber) <= 11),
    Date                datetime,
    State               int check (State = 0 or State = 1) not null,
    LIDs                int,
)

create table BookLog
(
    BookIDs      int                                not null,
    CustomerIDs  int                                not null,
    DateBorrow   datetime,
    LIDsCheckIn  int,
    DateReturn   datetime,
    LIDsCheckOut int,
    -- DateDue date,
    State        int check (State = 0 or State = 1) not null,
)

create table Librarian
(
    LibrarianIDs         int primary key IDENTITY (1, 1),
    LibrarianName        nvarchar(50)                       not null,
    LibrarianAge         int check (22 <= LibrarianAge and LibrarianAge <= 80),
    LibrarianSex         nvarchar(10) check (LibrarianSex = 'Male' or LibrarianSex = 'Female'),
    LibrarianPhoneNumber nvarchar(12) check (10 <= LEN(LibrarianPhoneNumber) and LEN(LibrarianPhoneNumber) <= 11),
    DateEnrol            datetime,
    DateRetire           datetime,
    State                int check (State = 0 or State = 1) not null,
)

create table Scheduled
(
    DateOfWeek   nvarchar(11),
    TimeStart    time,
    TimeEnd      time,
    LibrarianIDs int,
)

-- alter table Book
--     add LIDs int
-- alter table Customer
--     add LIDs int
-- alter table BookLog
--     add LIDs int

alter table Book
    add constraint FK_B_LIDs foreign key (LIDs) references Librarian (LibrarianIDs)

alter table Customer
    add constraint FK_C_LIDs foreign key (LIDs) references Librarian (LibrarianIDs)

alter table BookLog
    add constraint FK_BLI_LIDs foreign key (LIDsCheckIn) references Librarian (LibrarianIDs)

alter table BookLog
    add constraint FK_BLO_LIDs foreign key (LIDsCheckOut) references Librarian (LibrarianIDs)

alter table BookLog
    add constraint FK_B_BookLog foreign key (BookIDs) references Book (BookIDs)

alter table BookLog
    add constraint FK_C_BookLog foreign key (CustomerIDs) references Customer (CustomerIDs)

alter table BookLog
    add constraint PK_BookLog primary key (CustomerIDs, BookIDs)

alter table Scheduled
    add constraint FK_Scheduled foreign key (LibrarianIDs) references Librarian (LibrarianIDs)

INSERT INTO Book (BookName, BookAuthor, BookCategory, BookAmountAvailable, BookAmountBorrowed, Date, State, LIDs)
VALUES ('C#', 'Thomas', 'IT', 20, 0, current_timestamp, 0, null)

INSERT INTO Book (BookName, BookAuthor, BookCategory, BookAmountAvailable, BookAmountBorrowed, Date, State, LIDs)
VALUES ('C++', 'Thomas', 'IT', 30, 0, current_timestamp, 0, null)

INSERT INTO Book (BookName, BookAuthor, BookCategory, BookAmountAvailable, BookAmountBorrowed, Date, State, LIDs)
VALUES ('Python and C++', 'Jack', 'IT', 10, 0, current_timestamp, 0, null)

INSERT INTO Book (BookName, BookAuthor, BookCategory, BookAmountAvailable, BookAmountBorrowed, Date, State, LIDs)
VALUES ('Rust', 'Theo', 'IT', 40, 0, current_timestamp, 0, null)

INSERT INTO Book (BookName, BookAuthor, BookCategory, BookAmountAvailable, BookAmountBorrowed, Date, State, LIDs)
VALUES ('Javascript', 'Theo', 'IT', 50, 0, current_timestamp, 0, null)

INSERT INTO Book (BookName, BookAuthor, BookCategory, BookAmountAvailable, BookAmountBorrowed, Date, State, LIDs)
VALUES ('Python in the nutshell', 'Jack', 'IT', 20, 0, current_timestamp, 0, null)

INSERT INTO Book (BookName, BookAuthor, BookCategory, BookAmountAvailable, BookAmountBorrowed, Date, State, LIDs)
VALUES ('Java', 'Jack', 'IT', 30, 0, current_timestamp, 0, null)

INSERT INTO Book (BookName, BookAuthor, BookCategory, BookAmountAvailable, BookAmountBorrowed, Date, State, LIDs)
VALUES ('SQL Server', 'Thomas', 'IT', 20, 0, current_timestamp, 0, null)

INSERT INTO Book (BookName, BookAuthor, BookCategory, BookAmountAvailable, BookAmountBorrowed, Date, State, LIDs)
VALUES ('MySQL', 'Thomas', 'IT', 20, 0, current_timestamp, 0, null)

INSERT INTO Book (BookName, BookAuthor, BookCategory, BookAmountAvailable, BookAmountBorrowed, Date, State, LIDs)
VALUES ('Firebase and other cloud systems', 'Thomas', 'IT', 20, 0, current_timestamp, 0, null)

select LibrarianName, Date, TimeStart, TimeEnd
from (Librarian left join Scheduled on Librarian.LibrarianIDs = Scheduled.LibrarianIDs)

select Book.BookIDs,
       BookName,
       BookAuthor,
       BookCategory,
       BookAmountAvailable,
       BookAmountBorrowed,
       Book.Date,
       CustomerIDs
from (Book left join BookLog on BookLog.BookIDs = Book.BookIDs and BookLog.State = 0)
where cast(Book.Date as date) = '2022/10/07'
  and Book.State = 0

select Book.BookIDs,
       BookName,
       BookAuthor,
       BookCategory,
       BookAmountAvailable,
       BookAmountBorrowed,
       Book.Date,
       CustomerIDs
from (Book left join BookLog on BookLog.BookIDs = Book.BookIDs and BookLog.State = 0)
where Book.Date between '2022-10-01 18:29:12.000' and '2022-10-07 23:59:59.000'
  and Book.State = 0

select Book.BookIDs,
       BookName,
       BookAuthor,
       BookCategory,
       BookAmountAvailable,
       BookAmountBorrowed,
       Book.Date,
       CustomerIDs
from (Book left join BookLog on BookLog.BookIDs = Book.BookIDs and BookLog.State = 0)
where datediff(month, Book.Date, '10/9/2022') = 2
  and Book.State = 0

select LibrarianName, Librarian.LibrarianIDs
from (Scheduled left join Librarian on Librarian.LibrarianIDs = Scheduled.LibrarianIDs)
where '15:00' between TimeStart and TimeEnd
  and DateOfWeek = 'Monday'

-- select Customer.CustomerIDs,
--        CustomerName,
--        CustomerAge,
--        CustomerSex,
--        CustomerPhoneNumber,
--        Customer.Date,
--        BookAmount.BookIDs
-- from (Customer left join BookAmount on Customer.CustomerIDs = BookAmount.CustomerIDs and BookAmount.State = 0)
-- where Customer.State = 0
--   and Customer.CustomerIDs = 8
-- 
-- select Customer.CustomerIDs,
--        CustomerName,
--        CustomerAge,
--        CustomerSex,
--        CustomerPhoneNumber,
--        Customer.Date,
--        BookAmount.BookIDs
-- from (Customer left join BookAmount on Customer.CustomerIDs = BookAmount.CustomerIDs and BookAmount.State = 0)
-- where Customer.State = 0
-- 
-- Select BookAmount.BookIDs, CustomerIDs, BookAmountAvailable, BookAmountBorrowed
-- from (BookAmount left join Books on Books.BookIDs = BookAmount.BookIDs)
-- where BookAmount.BookIDs = 1
--   and CustomerIDs = 9

-- select (select distinct DateOfWeek from Scheduled)
-- from Scheduled

select DateOfWeek, TimeStart, TimeEnd
from (Scheduled left join Librarian on Librarian.LibrarianIDs = Scheduled.LibrarianIDs)
where DateOfWeek in ('Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday', 'Saturday', 'Sunday')
  and Scheduled.LibrarianIDs IS NULL

-- select Customer.CustomerIDs,
--        CustomerName,
--        CustomerAge,
--        CustomerSex,
--        CustomerPhoneNumber,
--        Customer.Date,
--        Customer.State,
--        BookAmount.BookIDs
-- from (Customer left join BookAmount on Customer.CustomerIDs = BookAmount.CustomerIDs)
-- where Customer.State = 0
--   and CustomerName in 'P%'

-- select Books.BookIDs,
--        Books.BookName,
--        Books.BookAuthor,
--        Books.BookCategory,
--        Books.BookAmountAvailable,
--        Books.BookAmountBorrowed,
--        Books.Date,
--        Books.State,
--        BookAmount.BookIDs,
--        BookAmount.CustomerIDs,
--        BookAmount.Date,
--        BookAmount.State
-- from (Books left join BookAmount on BookAmount.BookIDs = Books.BookIDs)
-- where Books.State = 0
--   and BookAmount.State = 0

-- INSERT INTO Customer (CustomerName, CustomerAge, CustomerSex, CustomerPhoneNumber, CustomerStatus)
-- VALUES ('John', 20, 'Male', '0900900900', '')
-- 
-- INSERT INTO Customer (CustomerName, CustomerAge, CustomerSex, CustomerPhoneNumber, CustomerStatus)
-- VALUES ('Math', 20, 'Male', '0900900900', '')
-- 
-- INSERT INTO Customer (CustomerName, CustomerAge, CustomerSex, CustomerPhoneNumber, CustomerStatus)
-- VALUES ('Betty', 20, 'Female', '0900900900', '')
-- 
-- INSERT INTO Customer (CustomerName, CustomerAge, CustomerSex, CustomerPhoneNumber, CustomerStatus)
-- VALUES ('Sin', 20, 'Male', '0900900900', '')

-- select Customer.CustomerIDs, CustomerName, CustomerAge, CustomerSex, CustomerPhoneNumber, CustomerStatus
-- from Customer,
--      BookAmount
-- where Customer.CustomerIDs = BookAmount.CustomerIDs
-- group by Customer.CustomerIDs, CustomerName, CustomerAge, CustomerSex, CustomerPhoneNumber, CustomerStatus
-- 
-- select Books.BookIDs,
--        BookName,
--        BookAuthor,
--        BookCategory,
--        BookAmountAvailable,
--        BookAmountBorrowed,
--        Books.Date,
--        BookAmount.CustomerIDs,
--        BookAmount.Date
-- from Books,
--      BookAmount
-- where Books.BookIDs = BookAmount.BookIDs
-- order by Books.BookIDs, CustomerIDs, BookAmount.Date
-- 
-- select Customer.CustomerIDs, CustomerName, CustomerAge, CustomerSex, CustomerPhoneNumber, BookAmount.BookIDs
-- from Customer,
--      BookAmount
-- where Customer.CustomerIDs = BookAmount.CustomerIDs
-- order by Customer.CustomerIDs, BookAmount.BookIDs

-- select Books.BookIDs, BookName, BookAuthor, BookCategory, BookAmountAvailable, BookAmountBorrowed, Books.Date,
--        case when exists(select * where Books.BookIDs = BookAmount.BookIDs) 
--            then BookAmount.CustomerIDs 
--            else 0 
--            end as bool1
-- 
-- from Books, BookAmount
-- -- group by Books.BookIDs, BookName, BookAuthor, BookCategory, BookAmountAvailable, BookAmountBorrowed, Books.Date, BookAmount.BookIDs, BookAmount.CustomerIDs,
-- -- where Books.BookIDs = BookAmount.BookIDs and Books.BookIDs = 1
-- order by Books.BookIDs

-- select IDs,
--        Name,
--        Author,
--        Cate,
--        Available,
--        Borrowed,
--        Dat,
--        bool1
-- from (select Books.BookIDs                                                                             as IDs,
--              BookName                                                                                  as Name,
--              BookAuthor                                                                                as Author,
--              BookCategory                                                                              as Cate,
--              BookAmountAvailable                                                                       as Available,
--              BookAmountBorrowed                                                                        as Borrowed,
--              Books.Date                                                                                as Dat,
--              IIF(exists(select * where Books.BookIDs = BookAmount.BookIDs), BookAmount.CustomerIDs, 0) as bool1
-- 
--       from Books,
--            BookAmount) as a
-- 
-- group by IDs, Name, Author, Cate, Available, Borrowed, Dat, bool1
-- -- where Books.BookIDs = BookAmount.BookIDs and Books.BookIDs = 1
-- -- order by IDs
-- 
-- select Books.BookIDs,
--        BookName,
--        BookAuthor,
--        BookCategory,
--        BookAmountAvailable,
--        BookAmountBorrowed,
--        Books.Date,
--        BookAmount.CustomerIDs,
--        BookAmount.Date
-- from Books,
--      BookAmount
-- where Books.BookIDs = BookAmount.BookIDs
-- order by Books.BookIDs, CustomerIDs, BookAmount.Date

-- delete from BookAmount where BookAmount.BookIDs = 2 and BookAmount.CustomerIDs = 0

-- select BookIDs, CustomerIDs, Date
-- from BookAmount

-- Select BookIDs, BookName, BookAuthor, BookCategory, BookAmount, Date, CustomerIDs from Books where BookName like '1%'

-- select Books.BookIDs,
--        BookName,
--        BookAuthor,
--        BookCategory,
--        BookAmountAvailable,
--        BookAmountBorrowed,
--        Books.Date,
--        CustomerIDs
-- from (Books left join BookAmount on BookAmount.BookIDs = Books.BookIDs)
-- where Books.BookIDs = 1
-- 
-- select Books.BookIDs,
--        BookName,
--        BookAuthor,
--        BookCategory,
--        BookAmountAvailable,
--        BookAmountBorrowed,
--        Books.Date,
--        CustomerIDs
-- from (Books left join BookAmount on BookAmount.BookIDs = Books.BookIDs)
-- where BookAmountAvailable > 0
-- 
-- select Books.BookIDs,
--        BookName,
--        BookAuthor,
--        BookCategory,
--        BookAmountAvailable,
--        BookAmountBorrowed,
--        Books.Date,
--        CustomerIDs
-- from (Books left join BookAmount on BookAmount.BookIDs = Books.BookIDs)
-- where Books.Date between '09/30/2022 00:00:00' and '10/03/2022 23:59:59'
-- 
-- delete
-- from Customer
-- where CustomerName = 'Betty'
-- 
-- select BookName, case when exists(select * where Books.BookIDs = 2) then 1 else 0 end as Bool
-- from Books
-- -- where BookIDs = 2
-- -- where Books.Date between '2022/10/03 00:00:00' and '2022/10/03 23:59:59.999'
-- 
-- select Customer.CustomerIDs, CustomerName, CustomerAge, CustomerSex, CustomerPhoneNumber, BookIDs
-- from (Customer left join BookAmount on Customer.CustomerIDs = BookAmount.CustomerIDs)
-- where Customer.CustomerIDs = 1
-- 
-- -- select Books.BookIDs,
-- --        BookName,
-- --        BookAuthor,
-- --        BookCategory,
-- --        BookAmountAvailable,
-- --        BookAmountBorrowed,
-- --        Books.Date,
-- --        CustomerIDs
-- -- from (Books left join BookAmount on BookAmount.BookIDs = Books.BookIDs)
-- -- where cast(Books.Date as date) = '2022/06/10'
-- -- where Books.Date between '2022/06/10 00:00' and '2022/06/10 23:59:59'
-- --   and Books.State = 0
-- 
-- update Books
-- set BookAmountAvailable = 19,
--     BookAmountBorrowed  = 1
-- where BookIDs = 3

-- select Count(BookIDs)
-- from Books
-- where Books.State = 0
-- 
-- -- declare @max int
-- -- select @max = max(BookIDs) from Books
-- -- if @max IS NULL   --check when max is returned as null
-- --     SET @max = 0
-- -- 
-- Dbcc checkident (Books, reseed, 0)
