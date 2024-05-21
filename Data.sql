insert into [TicketManageDB].[dbo].[Users] ([UserName]     , [Email]) values ('btopham0', 'dcamies0@slideshare.net');
insert into [TicketManageDB].[dbo].[Users] ([UserName]     , [Email]) values ('rgilliatt1', 'mharley1@google.es');
insert into [TicketManageDB].[dbo].[Users] ([UserName]     , [Email]) values ('rhemshall2', 'mpetley2@illinois.edu');
insert into [TicketManageDB].[dbo].[Users] ([UserName]     , [Email]) values ('jcastellanos3', 'cshatford3@bluehost.com');
insert into [TicketManageDB].[dbo].[Users] ([UserName]     , [Email]) values ('mpesak4', 'aohern4@deliciousdays.com');
insert into [TicketManageDB].[dbo].[Users] ([UserName]     , [Email]) values ('kniven5', 'hbonnor5@lulu.com');
insert into [TicketManageDB].[dbo].[Users] ([UserName]     , [Email]) values ('dhaukey6', 'emcgurgan6@unesco.org');
insert into [TicketManageDB].[dbo].[Users] ([UserName]     , [Email]) values ('gmarini7', 'arudolph7@sitemeter.com');
insert into [TicketManageDB].[dbo].[Users] ([UserName]     , [Email]) values ('dnorsworthy8', 'gmatcham8@geocities.com');
insert into [TicketManageDB].[dbo].[Users] ([UserName]     , [Email]) values ('brosentholer9', 'opapen9@dyndns.org');
insert into [TicketManageDB].[dbo].[Users] ([UserName]     , [Email]) values ('rcoppoa', 'awhaphama@delicious.com');
insert into [TicketManageDB].[dbo].[Users] ([UserName]     , [Email]) values ('eokieltb', 'ppatleyb@techcrunch.com');



insert into [TicketManageDB].[dbo].[Tickets] ([Title],[Description] ,[Status],[UserId]) values 
('title', 
'description',
'en cours',
1);

INSERT INTO [TicketManageDB].[dbo].[Roles] ([UserId], [Types])
VALUES (1, 'Admin'),
       (2, 'Admin'),
       (3, 'Admin'),
       (4, 'Admin'),
       (5, 'Admin'),
       (6, 'Admin');

INSERT INTO [TicketManageDB].[dbo].[Roles] ([UserId], [Types])
	   VALUES (7, 'User'),
       (8, 'User'),
       (9, 'User'),
       (10, 'User'),
       (11, 'User'),
       (12, 'User');