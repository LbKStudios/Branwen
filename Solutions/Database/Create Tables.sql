Create Table Files (
	ID UNIQUEIDENTIFIER,
	FileName VARCHAR(200),
	Extension VARCHAR(10),
	Size double,
	MediaType VARCHAR(20),
	DriveName VARCHAR(200));
Create Table FilePaths (
	FileID UNIQUEIDENTIFIER,
	PartOfPath INTEGER,
	Path VARCHAR(100));