create table Books
(
    BookIDs             int primary key not null IDENTITY (1,1),
    BookName            nvarchar(50),
    BookAuthor          nvarchar(50),
    BookCategory        nvarchar(50),
    BookAmountAvailable int check (0 <= BookAmountAvailable and BookAmountAvailable <= 100),
    BookAmountBorrowed  int check (BookAmountBorrowed >= 0),
    Date                datetime,
)

create table Customer
(
    CustomerIDs         int primary key not null IDENTITY (1,1),
    CustomerName        nvarchar(50),
    CustomerAge         int check (0 < CustomerAge and CustomerAge < 100),
    CustomerSex         nvarchar(10) check (CustomerSex = 'Male' or CustomerSex = 'Female'),
    CustomerPhoneNumber nvarchar(12) check (10 <= LEN(CustomerPhoneNumber) and LEN(CustomerPhoneNumber) <= 11),
    CustomerStatus      nvarchar(100),
)

create table BookAmount
(
    BookIDs     int not null,
    CustomerIDs int not null,
    Date        datetime,
)

alter table Books
    add constraint FK_CustomerIDs foreign key (CustomerIDs) references Customer (CustomerIDs)

alter table BookAmount
    add constraint PK_BookAmount primary key (CustomerIDs, BookIDs)

INSERT INTO Books (BookName, BookAuthor, BookCategory, BookAmountAvailable, BookAmountBorrowed, Date)
VALUES ('C#', 'Thomas', 'IT', 20, 0, null)

INSERT INTO Books (BookName, BookAuthor, BookCategory, BookAmountAvailable, BookAmountBorrowed, Date)
VALUES ('C++', 'Thomas', 'IT', 20, 0, null)

INSERT INTO Books (BookName, BookAuthor, BookCategory, BookAmountAvailable, BookAmountBorrowed, Date)
VALUES ('Python and C++', 'Jack', 'IT', 20, 0, null)

INSERT INTO Books (BookName, BookAuthor, BookCategory, BookAmountAvailable, BookAmountBorrowed, Date)
VALUES ('Rust', 'Theo', 'IT', 20, 0, null)

INSERT INTO Customer (CustomerName, CustomerAge, CustomerSex, CustomerPhoneNumber, CustomerStatus)
VALUES ('John', 20, 'Male', '0900900900', '')

INSERT INTO Customer (CustomerName, CustomerAge, CustomerSex, CustomerPhoneNumber, CustomerStatus)
VALUES ('Math', 20, 'Male', '0900900900', '')

INSERT INTO Customer (CustomerName, CustomerAge, CustomerSex, CustomerPhoneNumber, CustomerStatus)
VALUES ('Betty', 20, 'Female', '0900900900', '')

INSERT INTO Customer (CustomerName, CustomerAge, CustomerSex, CustomerPhoneNumber, CustomerStatus)
VALUES ('Sin', 20, 'Male', '0900900900', '')

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

select Books.BookIDs, BookName, BookAuthor, BookCategory, BookAmountAvailable, BookAmountBorrowed, Books.Date, CustomerIDs
from (Books left join BookAmount on BookAmount.BookIDs = Books.BookIDs)
where Books.BookIDs = 1

select Books.BookIDs, BookName, BookAuthor, BookCategory, BookAmountAvailable, BookAmountBorrowed, Books.Date, CustomerIDs
from (Books left join BookAmount on BookAmount.BookIDs = Books.BookIDs)
where BookAmountAvailable > 0

select Books.BookIDs, BookName, BookAuthor, BookCategory, BookAmountAvailable, BookAmountBorrowed, Books.Date, CustomerIDs
from (Books left join BookAmount on BookAmount.BookIDs = Books.BookIDs)
where Books.Date between '09/30/2022 00:00:00' and '10/03/2022 23:59:59'

delete from Customer where CustomerName = 'Betty'

select BookName, case when exists(select * where Books.BookIDs = 2) then 1 else 0 end as Bool from Books where BookIDs = 2

-- where Books.Date between '2022/10/03 00:00:00' and '2022/10/03 23:59:59.999'

select Customer.CustomerIDs, CustomerName, CustomerAge, CustomerSex, CustomerPhoneNumber, BookIDs
from (Customer left join BookAmount on Customer.CustomerIDs = BookAmount.CustomerIDs)
where Customer.CustomerIDs = 1

update Books set BookAmountAvailable = 19, BookAmountBorrowed = 1 where BookIDs = 3

declare @max int
select @max = max(BookIDs) from Books
if @max IS NULL   --check when max is returned as null
    SET @max = 0

DBCC CHECKIDENT (Books, RESEED, @max)

Dbcc checkident ( [Books], reseed, 0)

create trigger BooksTrigger on Books for DELETE as
Begin
    Select BookIDs from Books
end