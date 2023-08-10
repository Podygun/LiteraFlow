create table if not exists users(
	UserId serial int primary key,
	Email varchar(64),
	Password varchar(100),
	Salt varchar(100),
	Status int	
)
create index if not exists ix_user_email on users(Email);

create table if not exists usersecurity(
	UserSecurityId serial int primary key,
	UserId int references Users (UserId),
	VerificationCode varchar(64)
)

create table if not exists profiles ( 
	ProfileId serial int PRIMARY KEY,
	UserId int REFERENCES Users (UserId),
	FullName varchar (128),
	ProfileImage varchar(128),
	DateBirth date,
	Gender varchar (7),
	About varchar (512),
	ContactsId int references UserContacts (UserContactId)
)

create table if not exists EmailQueue(
	EmailQueueId serial int primary key,
	EmailTo varchar(128),
	EmailFrom varchar(128),
	EmailSubject varchar(128),
	EmailBody Text,
	CreatedOn time,
	ProcessingId varchar(100),
	Retry int
)

