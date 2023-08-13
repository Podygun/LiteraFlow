CREATE TABLE IF NOT EXISTS PermissionTypes
(
    PermissionTypeId serial primary key,
	Title varchar(32) NOT NULL,
	Description varchar(64) NULL
)