USE [Tansola]
GO
/****** Object:  Table [dbo].[App_DataSpec]    Script Date: 8/20/2022 9:35:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[App_DataSpec](
	[SpecID] [int] IDENTITY(1,1) NOT NULL,
	[SpecIndex] [int] NULL,
	[Status] [varchar](50) NULL,
	[TableName] [varchar](50) NULL,
	[ColumnName] [varchar](50) NULL,
	[ColIndex] [varchar](50) NULL,
	[ControlLabel] [varchar](50) NULL,
	[ControlType] [varchar](50) NULL,
	[ControlValidate] [varchar](50) NULL,
	[ControlStyle] [varchar](100) NULL,
	[ControlSize] [varchar](50) NULL,
	[ControlTitle] [varchar](100) NULL,
	[RuleNotes] [varchar](max) NULL,
 CONSTRAINT [PK_App_DataSpec] PRIMARY KEY CLUSTERED 
(
	[SpecID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[App_Log]    Script Date: 8/20/2022 9:35:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[App_Log](
	[RowID] [int] IDENTITY(1,1) NOT NULL,
	[LogType] [varchar](50) NULL,
	[DateTime] [smalldatetime] NULL,
	[Url] [varchar](100) NULL,
	[UserID] [varchar](50) NULL,
	[LogInfo] [varchar](max) NULL,
 CONSTRAINT [PK_App_Control] PRIMARY KEY CLUSTERED 
(
	[RowID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[App_Lookup]    Script Date: 8/20/2022 9:35:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[App_Lookup](
	[Category] [nvarchar](50) NULL,
	[Status] [nvarchar](50) NULL,
	[CodeValue] [nvarchar](50) NULL,
	[CodeDisplay] [nvarchar](50) NULL,
	[ShowOrder] [int] NULL,
	[RowID] [int] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [PK_App_Lookup] PRIMARY KEY CLUSTERED 
(
	[RowID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[App_Rules]    Script Date: 8/20/2022 9:35:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[App_Rules](
	[RuleID] [int] IDENTITY(1,1) NOT NULL,
	[RuleStatus] [varchar](50) NULL,
	[TableName] [varchar](50) NULL,
	[RuleType] [varchar](50) NULL,
	[RuleOrder] [int] NULL,
	[RuleSql] [varchar](max) NULL,
	[ActionSql] [varchar](max) NULL,
	[SpecID] [int] NULL,
	[RuleSecurity] [int] NULL,
	[RuleNotes] [varchar](max) NULL,
 CONSTRAINT [PK_App_Rules] PRIMARY KEY CLUSTERED 
(
	[RuleID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[App_UserRole]    Script Date: 8/20/2022 9:35:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[App_UserRole](
	[RowID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [nvarchar](50) NULL,
	[RoleCode] [nvarchar](50) NULL,
 CONSTRAINT [PK_App_UserRole] PRIMARY KEY CLUSTERED 
(
	[RowID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[App_UserRoleSpec]    Script Date: 8/20/2022 9:35:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[App_UserRoleSpec](
	[RoleCode] [varchar](50) NULL,
	[SpecID] [int] NULL,
	[Security] [int] NULL,
	[RowID] [int] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [PK_App_RoleSpec] PRIMARY KEY CLUSTERED 
(
	[RowID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Customer]    Script Date: 8/20/2022 9:35:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customer](
	[CustomerID] [int] IDENTITY(1,1) NOT NULL,
	[Status] [nvarchar](50) NULL,
	[ProjectTitle] [nvarchar](50) NULL,
	[CustomerCode] [nvarchar](50) NULL,
	[CompanyName] [nvarchar](50) NULL,
	[ContactName] [nvarchar](50) NULL,
	[Address] [nvarchar](50) NULL,
	[City] [nvarchar](50) NULL,
	[Region] [nvarchar](50) NULL,
	[Zip] [nvarchar](50) NULL,
	[State] [nvarchar](50) NULL,
	[Phone] [nvarchar](50) NULL,
	[Email] [nvarchar](50) NULL,
	[UserID] [nvarchar](50) NULL,
	[StartDate] [datetime] NULL,
	[EndDate] [datetime] NULL,
	[Budget] [money] NULL,
	[Spend] [money] NULL,
	[Extra] [nvarchar](50) NULL,
	[Notes] [nvarchar](max) NULL,
	[RowLog] [nvarchar](max) NULL,
	[RowUpdate] [nvarchar](50) NULL,
	[RowID] [int] NULL,
 CONSTRAINT [PK_Customer] PRIMARY KEY CLUSTERED 
(
	[CustomerID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Project]    Script Date: 8/20/2022 9:35:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Project](
	[ProjectID] [int] IDENTITY(1,1) NOT NULL,
	[Status] [varchar](50) NULL,
	[ProjectTitle] [varchar](50) NULL,
	[CustomerCode] [varchar](50) NULL,
	[CompanyName] [varchar](50) NULL,
	[ContactName] [varchar](50) NULL,
	[Address] [varchar](50) NULL,
	[City] [varchar](50) NULL,
	[Region] [varchar](50) NULL,
	[Zip] [varchar](50) NULL,
	[State] [varchar](50) NULL,
	[Phone] [varchar](50) NULL,
	[Email] [varchar](50) NULL,
	[UserID] [varchar](50) NULL,
	[StartDate] [datetime] NULL,
	[EndDate] [datetime] NULL,
	[Budget] [money] NULL,
	[Spend] [money] NULL,
	[Notes] [varchar](max) NULL,
	[RowLog] [varchar](max) NULL,
	[RowUpdate] [varchar](50) NULL,
	[RowID] [int] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Project_Spending]    Script Date: 8/20/2022 9:35:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Project_Spending](
	[ProjectID] [int] NULL,
	[Month] [int] NULL,
	[Year] [int] NULL,
	[Amount] [money] NULL,
	[SpendType] [nvarchar](50) NULL,
	[SpendDate] [datetime] NULL,
	[UserID] [nvarchar](50) NULL,
	[Notes] [nvarchar](50) NULL,
	[RowID] [int] IDENTITY(1,1) NOT NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[App_Log] ADD  CONSTRAINT [DF_App_Log_DateTime]  DEFAULT (getdate()) FOR [DateTime]
GO
/****** Object:  StoredProcedure [dbo].[sp_App_FormLoad]    Script Date: 8/20/2022 9:35:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
exec sp_App_FormLoad 'Master', 'CATHY', 'Project', '1', 'debug'
exec sp_App_FormLoad 'CATHY', 'Project', '1', 'debug'
*/

CREATE procedure    [dbo].[sp_App_FormLoad]
					@P1		varchar(50)  = '',	-- @Mode
					@P2 	varchar(50)  = '',	-- @UserID	
					@P3		varchar(50)  = '', 	-- @TableName = TableID
					@P4		varchar(50)  = '',  -- @ProjectID = RecordID
					@P5		varchar(50)  = '',	--
					@P6		varchar(50)  = '',	-- 
					@P7		varchar(50)  = '',	-- 
					@P8		varchar(50)  = '',	-- 
					@P9		varchar(50)  = '',	-- 
					@P10	varchar(50)  = ''	-- 
as

declare @Mode		varchar(50)		set @Mode		= @P1
declare @UserID		varchar(50)		set @UserID		= @P2
declare @TableName	varchar(50)		set @TableName	= @P3
declare @ProjectID	varchar(50)		set @ProjectID	= @P4

declare @Sql 		varchar(max)		

	
	IF OBJECT_ID('tempdb..#DataSpec_UserRole')	IS NOT NULL drop table #DataSpec_UserRole
	IF OBJECT_ID('tempdb..#final')	IS NOT NULL drop table #final
	
	select * into #DataSpec_UserRole from [dbo].[App_DataSpec] 
	where [TableName] = @TableName and [Status] = 'ACTIVE'

	alter table #DataSpec_UserRole add [UserID]	varchar(50), [Security] varchar(50), [RoleCode] varchar(50)
	
	-- Show step by step
	update #DataSpec_UserRole set [UserID] = @UserID, [Security] = '2' -- default
	
	update #DataSpec_UserRole set [RoleCode] = r.[RoleCode]
	from #DataSpec_UserRole u
	inner join [App_UserRole] r on u.[UserID] = r.[UserID]

	update #DataSpec_UserRole set [Security] = r.[Security]
	from #DataSpec_UserRole u
	inner join [App_UserRoleSpec] r on ( u.[RoleCode] = r.[RoleCode] and  u.[SpecID] = r.[SpecID] )

	update #DataSpec_UserRole set [RuleNotes] = 'Role Restriction' 	where [Security] < 2

	select * into #final from #DataSpec_UserRole

	
	/* Rules LATER
	-- -------------------------------------------------------------------------
		
	-- -------------------------------------------------------------------------

	-- Loop over the rules, check with #project and assign
	select S.[DbFieldName], R.* into #rules 
	from App_Rules R
	inner join App_DataSpec S on (S.[SpecID] = R.[SpecID])
	
	-- select * from #rules
	
	declare @RuleID			int
	declare @DbFieldName	varchar(50)
	declare @RuleSql		varchar(50)
	declare @RuleSecurity	varchar(50)
	declare @RuleNotes		varchar(50)

	DECLARE table_cursor CURSOR FOR
		SELECT [RuleID], [DbFieldName], [RuleSql], [RuleSecurity], [RuleNotes]
		FROM #rules where ([RuleType]='READ' and [RuleStatus] = 'ACTIVE')
		
	OPEN table_cursor

	FETCH NEXT FROM table_cursor into @RuleID, @DbFieldName, @RuleSql, @RuleSecurity, @RuleNotes

	WHILE @@FETCH_STATUS = 0
	BEGIN
		
		set @Sql = 'update #project set [RuleSqlFlag] = ''' + cast(@RuleID as varchar(10)) + ''' where ' + @RuleSql
		
		print @Sql
		--print 'Stop here'
		--return;

		exec (@Sql);

		if (@@RowCount > 0)
			begin
				print 'YES - ' + @Sql;
				update #final set	[Security] = @RuleSecurity, 
									[RuleNotes] = @RuleNotes 
							where	[DbFieldName] = @DbFieldName
			end	
		else
			begin
				print 'NO - ' + @Sql;
			end		
		
		-- -------------------------------------------------------------------------	
			
   		FETCH NEXT FROM table_cursor into @RuleID, @DbFieldName, @RuleSql, @RuleSecurity, @RuleNotes
	END

	CLOSE table_cursor
	DEALLOCATE table_cursor
	*/
	-- -------------------------------------------------------------------------
	select * from #final  
	-- -------------------------------------------------------------------------
	/*
	if (@Mode = 'debug')
	begin
		select * from #roleSpec
		select * from #rules
		select * from #project
	end
	*/

	IF OBJECT_ID('tempdb..#roleSpec')		IS NOT NULL drop table #roleSpec
	IF OBJECT_ID('tempdb..#rules')			IS NOT NULL drop table #rules
	IF OBJECT_ID('tempdb..#final')			IS NOT NULL drop table #final
	IF OBJECT_ID('tempdb..#project')		IS NOT NULL drop table #project
	IF OBJECT_ID('tempdb..#DataSpec_UserRole')	IS NOT NULL drop table #DataSpec_UserRole

	
	-- -------------------------------------------------------------------------
	

/*



*/
GO
/****** Object:  StoredProcedure [dbo].[sp_App_FormSave]    Script Date: 8/20/2022 9:35:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/*

Update 08.14.2022. Ready to go.

exec sp_App_FormSave 'PRODUCTION', 'Project', '1', 'CATHY', '|ProjectID=23~9~T29~NEW.East.Coast|ProjectID=23~10~T210~NEW299|ProjectID=23~11~_211~NEW2'
exec sp_App_FormSave 'debug', 'Project', '1', 'CATHY', '|ProjectID=23~9~T29~NEW.East.Coast|ProjectID=23~10~T210~NEW299|ProjectID=23~11~_211~NEW2'
exec sp_App_FormSave 'debug', 'Project', '1', 'NB58050', '|ProjectID=23~32~T_21~Happy Notes 6|ProjectID=23~18~N_218~1189.88|ProjectID=23~10~T_210~79121'

*/

CREATE procedure    [dbo].[sp_App_FormSave]
					@P1 	varchar(50)  = '',	-- @Mode  (debug)
					@P2		varchar(50)  = '', 	-- @TableName 
					@P3		varchar(50)  = '',  -- @ProjectID
					@P4		varchar(50)  = '',	-- @UserID	
					@P5		varchar(max) = '',	-- @ToSave
					@P6		varchar(50)  = '',	-- 
					@P7		varchar(50)  = '',	-- 
					@P8		varchar(50)  = '',	-- 
					@P9		varchar(50)  = '',	-- 
					@P10	varchar(50)  = ''	-- 
					
as

declare @Mode		varchar(50)		set @Mode		= @P1
declare @TableName	varchar(50)		set @TableName	= @P2
declare @ProjectID	varchar(50)		set @ProjectID	= @P3
declare @UserID		varchar(50)		set @UserID		= @P4
declare @ToSave		varchar(max)	set @ToSave		= @P5
declare @Sql 		varchar(max)	
declare @ColumnName	varchar(50)

declare @ToCheck	varchar(50)    set @ToCheck = 'zzz_' + @UserID

declare @RowID		int
declare @Chop		varchar(max)
declare @Pos		int
declare @NextPos	int

declare @fChop		varchar(max)
declare @fStartPos	int
declare @fEndPos	int

declare @SqlError	int

SET NOCOUNT OFF

/*

This is how it works

New rule: Update one table per execution. Column #ToSave.[TableName] should all be the same.

1. Create a temp table #tosave of project/s to be updated.
2. Split the data and insert #tosave table
3. Select the record into a temp table (@ToCheck)
4. Scan table App_Rules run sql/s against @ToCheck
   if any check query (check for fail condition ) return record/s. Then ALL is fails. Abort.
   if passed ( no records return ). Point sql to update real table.
	
	
 


5. After Saving is done. Scan table App_Rules to run TRIGGER 

	In any case, ALWAYS return to caller a table. Either data or Status table below.

	[ExecStatus]  = '' is ok, any other = fail
	[ExecMessage] = some message for UI
	[ExecInfo]    = if OK	: |controlID|  
	                if ERROR: |controlID:ERROR| 

*/

-- 1. Clean up & Prepare Data ----------------------------------------------------------------
-- set @ToSave = replace(@ToSave, '^', '"')

declare @updateSql		varchar(100)		
declare @setSql			varchar(max)		
declare @whereSql		varchar(100)		

declare @SpecID		    varchar(50)	
declare @KeyInfo		varchar(50)	 -- IMPORTANT	ProjectID=23
--declare @TableName	varchar(50)	
--declare @ColumnName	varchar(50)	
declare @ControlID		varchar(50)	
declare @ControlType	varchar(50)	


declare @SaveValue			varchar(max) = ''	
declare @ControlsToUpdate	varchar(max) = ''
declare @RuleType		varchar(50)
declare @RuleID			int
declare @RuleSql		varchar(max)		-- rules to reject
declare @ActionSql		varchar(max)		-- Trigger sql
declare @RuleNotes		varchar(250)
declare @LastUpdate		varchar(50)


declare @ShortDate      varchar(50)

declare @OldValue		varchar(50)
declare @NewValue		varchar(50)
declare @Monitor		varchar(100)
-----------------------------------------------------------------
declare @ExecStatus 	varchar(50) = ''	-- Master Flag * * * 
declare @ExecMessage	varchar(max) = ''	
declare @ExecInfo		varchar(max) = ''
-----------------------------------------------------------------

declare @c varchar(1)
set @c = Right(@ToSave, 1)
if (@c <> '|')
begin
	set @ToSave = @ToSave + '|'	-- Just to make sure to add a | to the end of DataStr for the split function below to work. 
end 


-- select @LastUpdate = substring(convert(varchar(25), getdate(), 120), 1 ,16)  + '.' + @UserID
-- Long format for RowLog
select @LastUpdate = substring(convert(varchar(30), getdate(), 120), 1 ,10) + '. ' + @UserID
		-- '@' + substring(convert(varchar(30), getdate(), 120), 12 ,5) + '.' + @UserID

-- ShortFormat for RowNotes
select @ShortDate = convert(varchar(20),getdate(),111) + '. ' + @UserID
select @ShortDate = replace(@ShortDate, '/', '-');   -- for better readability

IF (EXISTS (
	SELECT * FROM INFORMATION_SCHEMA.TABLES  where TABLE_NAME = @ToCheck   -- zzz_CATHY
	))
BEGIN
    exec('drop table ' + @ToCheck)
END


-- A temp table	
create table #temp
(
   	[OldValue]	varchar(max),
   	[NewValue]	varchar(max)
)
insert #temp([OldValue],[NewValue]) values('','')  -- always has one blank record

-- -----------------------------------------------------------------------------------------
-- Create a temp table and split data into it. 
create table #tosave
(
   	SpecID		varchar(50),
   	KeyInfo		varchar(100),
   	TableName	varchar(100),
	ColumnName	varchar(100),
	SaveValue	varchar(1000),
	ControlID	varchar(50),
	ControlType	varchar(50),
	RuleType	varchar(50),      -- mainly to hold the 'MONITOR' from RULES table
	ToSave		varchar(max),
	RowUpdate	varchar(50),
	RuleNotes	varchar(100),
	ExecStatus  varchar(1000),
	ExecMessage varchar(1000),
	RowId		int IDENTITY (1, 1) PRIMARY KEY   
)

-- 1. Split the records -------------------------------------------
DECLARE  @len int, @i int=1, @N int=1, @f int=0
set @Chop = ''
set @Pos = charindex('|', @ToSave)
set @NextPos = 1
 
--  loop while there is still a ~ in the String 
-- '[Location]=^Atlanta, GA^~[Division]=^IT-Tech^~[DateEnd]=^01/11/2015^~'

while (@Pos <>  0) 
 
begin
		set @Chop = substring(@ToSave, 1, @Pos - 1)
		if (@Chop <> '') 
		begin
			insert into #tosave(ToSave) values(@Chop)
		end

		set @ToSave = substring(@ToSave, @Pos + 1, len(@ToSave))
		set @NextPos    =  @Pos
		set @Pos        = charindex('|', @ToSave)
end

-- 2. Split the cols----------------------------------------------------------------

DECLARE table_cursor CURSOR FOR
	select [RowID], [ToSave] from #tosave  
	
OPEN table_cursor
	FETCH NEXT FROM table_cursor into @RowID, @ToSave
	-- print '@ToSave : ' + @ToSave			

WHILE @@FETCH_STATUS = 0
BEGIN
	
	-- ---------------------------------	
	set @ToSave = @ToSave + '~';   
	set @i = 1 
	set @N = 1     
	set @f = 0
	SET @len = len(@ToSave) 
	
	-- ???? print cast(@i as varchar(10)) + '- - ' + cast(@len as varchar(10)) 
				
	WHILE (@i <= @len )
	BEGIN
			
			IF SUBSTRING(@ToSave, @i, 1) = '~' 
			BEGIN 
			   set @f = @f + 1 
			   SET @Chop = SUBSTRING( @ToSave, @N, @i-@N ) 
			   
			   if (@f = 1) set @KeyInfo		= @Chop
			   if (@f = 2) set @SpecID		= @Chop
			   if (@f = 3) set @ControlID   = @KeyInfo + '~' + @SpecID + '~' + @Chop
			   if (@f = 4) set @SaveValue	= @Chop

			   update #tosave set [SpecID] = @SpecID, [KeyInfo] = @KeyInfo, 
				                  [ControlID] = @ControlID, [SaveValue] = @SaveValue  
			   where [RowID] = @RowID

			   SET @N = @i+1 
			   
			END 
			SET @i = @i+1 
			
	END 
	
	-- ---------------------------------
	FETCH NEXT FROM table_cursor into @RowID, @ToSave
	
END

CLOSE table_cursor
DEALLOCATE table_cursor


-- ----------------------------------------------------------------
update #tosave set [TableName] = s.[TableName], [ColumnName] = s.[ColumnName], [ControlType] = s.[ControlType]
from #tosave d
inner join App_DataSpec s on d.[SpecID] = s.[SpecID] 

update #tosave set [RuleType] = 'MONITOR' 
from #tosave d
inner join App_Rules r on d.[SpecID] = r.[SpecID] and r.[RuleType] = 'MONITOR'
 
if (@Mode <> 'PRODUCTION') select * from #tosave
print 'created #tosave'
-- ---------------------------------------------------------------

-- ----------------------------------------------------------------
-- Query the selected project into temp table (@ToCheck)to test it with all the SAVE rules  

set @Sql = 'select ' + @TableName  + '.*, space(50) as [RuleSqlFlag] into ' + @ToCheck + ' from [' + @TableName + '] where ' + @KeyInfo
print @Sql
exec(@sql)

-------------------------------------------------------------------

		print '1. Performing Updating @ToCheck ----------------'
		BEGIN TRY    -- There is still a chance for updating error
		
			-- Multi-Users. Check if the record version is still good 
			-- ---------------------------------------------------------------
			
			DECLARE table_cursor CURSOR FOR
			select [SpecID], [KeyInfo], [TableName], [ColumnName], [SaveValue], [ControlID]
			from #tosave

			OPEN table_cursor
			FETCH NEXT FROM table_cursor into @SpecID, @KeyInfo, @TableName, @ColumnName, @SaveValue, @ControlID

			WHILE @@FETCH_STATUS = 0
			BEGIN
			
				set @Sql = 'update ' + @ToCheck + ' set [' + @ColumnName + '] = ''' + @SaveValue + ''' where ' + @KeyInfo
				exec (@Sql);
				print 'SP_1000. @ToCheck. Done. ' + @Sql
				
			-- ---------------------------------
			FETCH NEXT FROM table_cursor into @SpecID, @KeyInfo, @TableName, @ColumnName, @SaveValue, @ControlID

			END
		
		END TRY
			
		BEGIN CATCH
			
			set @ExecStatus  = 'ERROR'
			set @ExecMessage = '1000. Failed testing RuleID: ' + cast(@SpecID as varchar(20)) + '. ' + 
								@Sql + '. ' + ERROR_MESSAGE()
			set @ExecInfo	 = @ControlID + ':ERROR|1000. ' +  + ERROR_MESSAGE()
			print @ExecMessage
			CLOSE table_cursor
			DEALLOCATE table_cursor
			
			update #tosave set [ExecMessage] = @ExecMessage where [SpecID] = @SpecID
			
			select @ExecStatus as [ExecStatus], @ExecMessage as [ExecMessage], @ExecInfo as [ExecInfo]
			return -- Abort
			----------------------------------------------

			
		END CATCH
		
		CLOSE table_cursor
		DEALLOCATE table_cursor


-----------------------------------------------------------------------------------
print 'Now Start Checking all the rules against @ToCheck temp table ----------------------------'
-----------------------------------------------------------------------------------
DECLARE table_cursor CURSOR FOR
		select [RuleID], [RuleSql], [RuleNotes] from dbo.App_Rules 
			where [TableName] = @TableName and [RuleType] = 'SAVE' and [RuleStatus] = 'ACTIVE'
		
OPEN table_cursor
	FETCH NEXT FROM table_cursor into @RuleID, @RuleSql, @RuleNotes

	WHILE @@FETCH_STATUS = 0
	BEGIN
		-- ---------------------------------
		set @Sql = 'update ' + @ToCheck + ' set [RuleSqlFlag] = ' + cast(@RuleID as varchar(10)) + ' where ' +  @RuleSql
		print @Sql
		exec (@Sql);

		if (@@RowCount > 0)  -- if there is a match --> fail
			begin
				-- One fails > ALL fail.
				CLOSE table_cursor
				DEALLOCATE table_cursor
				
				set @ExecStatus  = 'ERROR'			-- * * *
				set @ExecMessage = '2000. Failed RuleID ' + cast(@RuleID as varchar(20)) + '. ' + @RuleNotes
				set @ExecInfo    = @ControlID + ':ERROR_2000. ' + @RuleNotes 
				----------------------------------------------
				update #tosave set [ExecMessage] = @ExecMessage where [SpecID] = @SpecID
				select @ExecStatus as [ExecStatus], @ExecMessage as [ExecMessage], @ExecInfo as [ExecInfo]
				return -- Abort
				----------------------------------------------
			end	
		else
			begin
				set @ExecMessage = 'OK. ' + @RuleSql
			end		
			
		-- ---------------------------------
		print @ExecMessage
		FETCH NEXT FROM table_cursor into @RuleID, @RuleSql, @RuleNotes
		
	END

CLOSE table_cursor
DEALLOCATE table_cursor

-- ---------------------------------------------------------------
print 'START 3000. Start updating the real table ----------------'

if (@ExecStatus = '')
begin
	-------------------------
	BEGIN TRAN
	-------------------------
		
		BEGIN TRY    -- There is still a chance for updating error (Ex: Constrain....)
			DECLARE table_cursor CURSOR FOR
			select SpecID, KeyInfo,	TableName, ColumnName, SaveValue, ControlID, ControlType, RuleType
			from #tosave

			OPEN table_cursor
				FETCH NEXT FROM table_cursor into @SpecID, @KeyInfo, @TableName, @ColumnName, @SaveValue, @ControlID, @ControlType, @RuleType

				WHILE @@FETCH_STATUS = 0
				BEGIN
					
					
					if (@RuleType = 'MONITOR')  
					begin
						-- exec(@Sql) won't work creating temp table
						set @Sql = 'update #temp set [OldValue] = (select [' + @ColumnName + '] from ' + @TableName + ' where ' + @KeyInfo + ')'
						print '[OldValue] 1. ' + @Sql
						exec(@Sql)
						
						set @Sql = 'update #temp set [NewValue] =  (select [' + @ColumnName + '] from ' + @ToCheck + ' where ' + @KeyInfo + ')'
						print '[NewValue] 2. ' + @Sql
						exec(@Sql)
												
						select @OldValue = [OldValue],  @NewValue = [NewValue]  from #temp
						
						select @Monitor = '<br>' + @LastUpdate + '. [' + @ColumnName + '] : ' + @OldValue + ' -> ' + @NewValue + '. ' 
						print '@Monitor: ' + @Monitor
						
						-- 2021.09.17 Add to Notes instead
						set @Sql = 'update ' + @TableName + ' set [RowLog] = ''' + @Monitor + '''  ' +
									' + [RowLog] where ' + @KeyInfo

						--print '[RowLog] ' + @Sql
						exec(@Sql)
						
					end
					
					-- ---------------------------------------------------------------
					set @Sql = ''
					if (@ControlType = 'notes')  
						-- add new notes to the top/front
						begin
							
							-- [Notes],[RowLog],[RowUpdate] are required columns

							-- 2021.09.22. Save the [Notes] to [RowLog] instead. 
							set @SaveValue = '<br>' + @ShortDate +  '. ' + @SaveValue + '. ' -- works ? + CHAR(13)+CHAR(10)
							set @Sql = 'update [' + @TableName + '] set [RowLog] = ''' + 
							@SaveValue + ''' +  [RowLog] , [RowUpdate]=''' + @LastUpdate + ''' where ' + @KeyInfo

							/*
							set @SaveValue = @ShortDate +  ': ' + @SaveValue + '. ' + char(10)
							set @Sql = 'update [' + @TableName + '] set [' + @ColumnName + '] = ''' + 
							@SaveValue + ''' +  [' + @ColumnName + '] , [RowUpdate]=''' + @LastUpdate + ''' where ' + @KeyInfo
							*/
							
						end
						
					else
						-- Normal cases, for the rest of the fields
						begin
							set @Sql = 'update [' + @TableName + '] set [' + @ColumnName + '] = ''' + @SaveValue
								+ ''', [RowUpdate]=''' + @LastUpdate + ''' where ' + @KeyInfo
						end
					
					exec (@Sql);
					print 'Real Done. ' + @Sql
					
					set @ControlsToUpdate = @ControlsToUpdate + @ControlID + '|'
					
					-- ---------------------------------
					FETCH NEXT FROM table_cursor into @SpecID, @KeyInfo, @TableName, @ColumnName, @SaveValue, @ControlID, @ControlType, @RuleType
					
				END
		
		END TRY
			
		BEGIN CATCH
			
			set @ExecStatus  = 'ERROR'			-- * * *
			set @ExecMessage = 'SP_3000. Failed RuleID ' + cast(@RuleID as varchar(20)) + '. ' + @RuleNotes
			set @ExecInfo    = @ControlID + ':' + @ExecMessage

			print @ExecMessage
			print ERROR_MESSAGE()
			CLOSE table_cursor
			DEALLOCATE table_cursor
			
			-------------------------
			ROLLBACK TRAN
			-------------------------
			select @ExecStatus as [ExecStatus], @ExecMessage as [ExecMessage], @ExecInfo as [ExecInfo]
			return -- Abort
			----------------------------------------------

			
		END CATCH
		
		CLOSE table_cursor
		DEALLOCATE table_cursor
	-------------------------
	COMMIT TRAN
	-------------------------	
end

--------------------------------------------------------------------------
print '@ControlsToUpdate : ' + @ControlsToUpdate

set @ExecStatus  = ''  -- should still be ''
set @ExecMessage = 'Update completed.' 
set @ExecInfo = @ControlsToUpdate

-- will return this after trigger. if any trigger fail -> indicate in @ExecMessage.
-- select @ExecStatus as [ExecStatus], @ExecMessage as [ExecMessage], @ExecInfo as [ExecInfo]
			

-- TRIGGERS --------------------------------------------------------------
-- Does it run multiple times. Every times ??? 02.02.2015
-- Add TRY CATCH later
declare @x int = 0

if (@ExecStatus = '')

begin
	print 'start Trigger'

	DECLARE table_cursor CURSOR FOR
			select [RuleID], [RuleSql], [ActionSql] from dbo.App_Rules 
				where [TableName] = @TableName and [RuleType] = 'TRIGGER' and [RuleStatus] = 'ACTIVE'
			
	OPEN table_cursor
		FETCH NEXT FROM table_cursor into @RuleID, @RuleSql, @ActionSql

		WHILE @@FETCH_STATUS = 0
		BEGIN
		
			set @Sql = 'update ' + @ToCheck + ' set [RuleSqlFlag] = ' + cast(@RuleID as varchar(10)) + ' where ' +  @RuleSql
			exec (@Sql)
			
 			if (@@ROWCOUNT > 0)  -- condition met for trigger
				begin
					print 'Trigger SQL: ' + @Sql
					exec (@ActionSql);	
					set @x = @x + 1
					print 'Trigger Done: ' + @ActionSql	
						
				end	
	
			-- ---------------------------------
			FETCH NEXT FROM table_cursor into @RuleID, @RuleSql, @ActionSql
			
		END

	CLOSE table_cursor
	DEALLOCATE table_cursor

	if ( @x > 0 ) set @ExecMessage = @ExecMessage + '. ' + cast(@x as varchar(10)) + ' trigger/s completed'
	
end

--------------------------------------------------------------------------
if (@Mode = 'debug')
	begin
		select 'tosave', #tosave.*     from #tosave
		--select 'tocheck', @ToCheck.*   from @ToCheck
		--select 'final', Project.* from Project where [ProjectID] = @ProjectID 
	end
--------------------------------------------------------------------------
IF OBJECT_ID('tempdb..#tosave')		IS NOT NULL drop table #tosave
IF OBJECT_ID('tempdb..#tocheck')	IS NOT NULL drop table #tocheck

select @ExecStatus as [ExecStatus], @ExecMessage as [ExecMessage], @ControlsToUpdate as [ExecInfo]

/*


*/
GO
/****** Object:  StoredProcedure [dbo].[sp_App_SideBar]    Script Date: 8/20/2022 9:35:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/*
exec sp_App_SideBar 'ShowSpec', 'Project', '11', 'CATHY'
*/

CREATE procedure    [dbo].[sp_App_SideBar]
					@P1 	varchar(50)  = '',	-- @Mode
					@P2		varchar(50)  = '', 	-- @TableName   (TableName)
					@P3		varchar(50)  = '',  -- @SpecID or RowID	   (Column's ID) 
					@P4		varchar(50)  = '',	-- @UserID
					@P5		varchar(50)  = '',	-- 
					@P6		varchar(50)  = '',	-- 
					@P7		varchar(50)  = '',	-- 
					@P8		varchar(50)  = '',	-- 
					@P9		varchar(50)  = '',	-- 
					@P10	varchar(50)  = ''	-- 

as
	
	declare @Mode		varchar(50)		set @Mode		= @P1
	declare @TableName	varchar(50)		set @TableName	= @P2
	declare @SpecID		varchar(50)		set @SpecID		= @P3
	declare @UserID		varchar(50)		set @UserID		= @P4
	
	declare @Sql 		varchar(max)		

	
	-- -----------------------------------------------

	if (@Mode = 'ShowSpec')
	begin
		SELECT [SpecID]
			   --,[SpecIndex]
			  ,[TableName]
			  -- ,[Status]
			  --,[ColIndex]
			  ,[ColumnName]
			  ,[ControlLabel]
      
			  ,[ControlType]
			  ,[ControlValidate]
			  --,[ControlClass]
			  --,[ControlStyle]
			  --,[ControlSize]
			  ,[ControlTitle]
			  --,[SaveToTable]
			  --,[SaveToField]
			  --,[RuleNotes]
		  FROM [App_DataSpec]
		  where [SpecID] = @SpecID
	  end 
	  
		if (@Mode = 'ShowDetail')
		begin
			SELECT	 [ProjectID]
					,[Month]
					,[Year]
					,[Amount]
					,[SpendType]
					,[SpendDate]
					,[UserID]
					,[Notes]
					,[RowID]
			FROM	 [Project_Spending]
			where	[RowID] = @SpecID  -- [RowID]
		end 
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	-- ============================================================================
	
	/*
	select * from #final order by [SpecIndex]
	drop table #final
	return
	*/

	/*
	-- -------------------------------------------------------------------------
	-- select * into #project from Project where [ProjectID] = @ProjectID 
	-- -------------------------------------------------------------------------
	-- Avoid select statement. [RuleSqlFlag] is a dummy field to test rules
	select Project.*, space(50) as [RuleSqlFlag] into #project from Project where [ProjectID] = @ProjectID 

	-- -------------------------------------------------------------------------
	
	-- -------------------------------------------------------------------------

	-- Loop over the rules, check with #project and assign
	select S.[ColumnName], R.* into #rules 
	from App_Rules R
	inner join App_DataSpec S on (S.[SpecID] = R.[SpecID])
	
	-- select * from #rules
	
	declare @RuleID			int
	declare @ColumnName	varchar(50)
	declare @RuleSql		varchar(50)
	declare @RuleSecurity	varchar(50)
	declare @RuleNotes		varchar(50)

	DECLARE table_cursor CURSOR FOR
		SELECT [RuleID], [ColumnName], [RuleSql], [RuleSecurity], [RuleNotes]
		FROM #rules where ([RuleType]='READ' and [RuleStatus] = 'ACTIVE')
		
	OPEN table_cursor

	FETCH NEXT FROM table_cursor into @RuleID, @ColumnName, @RuleSql, @RuleSecurity, @RuleNotes

	WHILE @@FETCH_STATUS = 0
	BEGIN
		
		set @Sql = 'update #project set [RuleSqlFlag] = ''' + cast(@RuleID as varchar(10)) + ''' where ' + @RuleSql
		
		print @Sql
		--print 'Stop here'
		--return;

		exec (@Sql);

		if (@@RowCount > 0)
			begin
				print 'YES - ' + @Sql;
				update #final set	[Security] = @RuleSecurity, 
									[RuleNotes] = @RuleNotes 
							where	[ColumnName] = @ColumnName
			end	
		else
			begin
				print 'NO - ' + @Sql;
			end		
		
		-- -------------------------------------------------------------------------	
			
   		FETCH NEXT FROM table_cursor into @RuleID, @ColumnName, @RuleSql, @RuleSecurity, @RuleNotes
	END

	CLOSE table_cursor
	DEALLOCATE table_cursor
	
	select * from #final  
	-- -------------------------------------------------------------------------
	
	if (@Mode = 'debug')
	begin
		-- select * from #roleSpec
		select * from #rules
		select * from #project
	end

	*/
	
	IF OBJECT_ID('tempdb..#maxSecurity')	IS NOT NULL drop table #maxSecurity
	IF OBJECT_ID('tempdb..#roleSpec')		IS NOT NULL drop table #roleSpec
	IF OBJECT_ID('tempdb..#rules')			IS NOT NULL drop table #rules
	IF OBJECT_ID('tempdb..#final')			IS NOT NULL drop table #final
	
	-- -------------------------------------------------------------------------
	

/*

exec sp_FormRead 'debug', 'Project', '23', 'NB58050'

set @Mode		= @P1
set @TableName	= @P2
set @ProjectID	= @P3
set @UserID		= @P4

*/
GO
