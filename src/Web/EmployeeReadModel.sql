USE LrmsDb

CREATE TABLE [dbo].[ReadModel-Employee](
	[Json] [nvarchar](max),

	-- -------------------------------------------------
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[AggregateId] [nvarchar](64) NOT NULL,

	CONSTRAINT [PK_ReadModel-Employee] PRIMARY KEY CLUSTERED 
	(
		[Id] ASC
	)
)

CREATE UNIQUE NONCLUSTERED INDEX [IX_ReadModel-EmployeeAggregate_AggregateId] ON [dbo].[ReadModel-Employee]
(
	[AggregateId] ASC
)