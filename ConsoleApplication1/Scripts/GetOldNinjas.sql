CREATE PROCEDURE [dbo].[GetOldNinjas]
AS
	SELECT n.*
	from dbo.Ninjas n
	where n.DateOfBirth < '20150101'
GO
