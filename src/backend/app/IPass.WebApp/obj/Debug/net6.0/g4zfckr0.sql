IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [ActionReasons] (
    [Id] uniqueidentifier NOT NULL,
    [ReasonName] nvarchar(max) NULL,
    [ReasonCategory] int NOT NULL,
    [Created] datetime2 NOT NULL,
    [Updated] datetime2 NOT NULL,
    CONSTRAINT [PK_ActionReasons] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [AppConfigs] (
    [Id] uniqueidentifier NOT NULL,
    [MaxOTPPerDay] int NOT NULL,
    [POBaseURL] nvarchar(max) NULL,
    [POAuthenticationUserName] nvarchar(max) NULL,
    [POAuthenticationPassword] nvarchar(max) NULL,
    [ParoBranchCode] nvarchar(max) NULL,
    [ParoAuthorizedCode] nvarchar(max) NULL,
    [ParoProcessType] nvarchar(max) NULL,
    [ParoSubCode] nvarchar(max) NULL,
    [ParoSendOTPParam1] nvarchar(max) NULL,
    [ParoValidateOTPParam1] nvarchar(max) NULL,
    [ParoOTPSendingUrl] nvarchar(max) NULL,
    [DengageLoginUrl] nvarchar(max) NULL,
    [DengageUserKey] nvarchar(max) NULL,
    [DengagePassword] nvarchar(max) NULL,
    CONSTRAINT [PK_AppConfigs] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Chats] (
    [Id] uniqueidentifier NOT NULL,
    [DemandId] uniqueidentifier NOT NULL,
    [CustomerId] uniqueidentifier NOT NULL,
    [ServicemanId] uniqueidentifier NOT NULL,
    [IsArchivedByServiceman] bit NOT NULL,
    [IsArchivedByCustomer] bit NOT NULL,
    [Created] datetime2 NOT NULL,
    [Updated] datetime2 NOT NULL,
    CONSTRAINT [PK_Chats] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Cities] (
    [Id] uniqueidentifier NOT NULL,
    [CityName] nvarchar(max) NULL,
    [IsHaveShopping] bit NOT NULL,
    [Created] datetime2 NOT NULL,
    [Updated] datetime2 NOT NULL,
    CONSTRAINT [PK_Cities] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [ContractTypes] (
    [Id] uniqueidentifier NOT NULL,
    [Name] nvarchar(max) NULL,
    [ContractFor] int NOT NULL,
    [Deleted] bit NOT NULL,
    [Created] datetime2 NOT NULL,
    [Updated] datetime2 NOT NULL,
    CONSTRAINT [PK_ContractTypes] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Counties] (
    [Id] uniqueidentifier NOT NULL,
    [CountyName] nvarchar(max) NULL,
    [CityId] uniqueidentifier NOT NULL,
    [IsHaveShopping] bit NOT NULL,
    [Created] datetime2 NOT NULL,
    [Updated] datetime2 NOT NULL,
    CONSTRAINT [PK_Counties] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Demands] (
    [Id] uniqueidentifier NOT NULL,
    [FocusedServicemanId] uniqueidentifier NULL,
    [RateComment_ServicemanId] uniqueidentifier NULL,
    [RateComment_Rate] int NULL,
    [RateComment_Comment] nvarchar(max) NULL,
    [DemandStatus] int NOT NULL,
    [Description] nvarchar(max) NULL,
    [CustomerId] uniqueidentifier NOT NULL,
    [Location_CityId] uniqueidentifier NULL,
    [Location_CountyId] uniqueidentifier NULL,
    [Location_DistrictId] uniqueidentifier NULL,
    [Category_MainCategoryId] uniqueidentifier NULL,
    [Category_SubCategoryId] uniqueidentifier NULL,
    [IsPublic] bit NOT NULL,
    [IsInsured] bit NOT NULL,
    [CancelReason_CancelId] uniqueidentifier NULL,
    [CancelReason_Detail] nvarchar(max) NULL,
    [Created] datetime2 NOT NULL,
    [Updated] datetime2 NOT NULL,
    CONSTRAINT [PK_Demands] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Districts] (
    [Id] uniqueidentifier NOT NULL,
    [DistrictName] nvarchar(max) NULL,
    [CountyId] uniqueidentifier NOT NULL,
    [Created] datetime2 NOT NULL,
    [Updated] datetime2 NOT NULL,
    CONSTRAINT [PK_Districts] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [InvalidNameCombinations] (
    [Id] uniqueidentifier NOT NULL,
    [Firstname] nvarchar(max) NULL,
    [Lastname] nvarchar(max) NULL,
    [Created] datetime2 NOT NULL,
    [Updated] datetime2 NOT NULL,
    CONSTRAINT [PK_InvalidNameCombinations] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [MainCategories] (
    [Id] uniqueidentifier NOT NULL,
    [Title] nvarchar(max) NULL,
    [Description] nvarchar(max) NULL,
    [IsActive] bit NOT NULL,
    [Insurable] bit NOT NULL,
    [IconUrl] nvarchar(max) NULL,
    [TaniRefId] nvarchar(max) NULL,
    [TaniServicemanRegisterRefId] nvarchar(max) NULL,
    [DisplayOrder] int NOT NULL,
    [Created] datetime2 NOT NULL,
    [Updated] datetime2 NOT NULL,
    CONSTRAINT [PK_MainCategories] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [SubCategories] (
    [Id] uniqueidentifier NOT NULL,
    [MainCategoryId] uniqueidentifier NOT NULL,
    [Title] nvarchar(max) NULL,
    [Description] nvarchar(max) NULL,
    [IsActive] bit NOT NULL,
    [Insurable] bit NOT NULL,
    [TaniRefId] nvarchar(max) NULL,
    [Created] datetime2 NOT NULL,
    [Updated] datetime2 NOT NULL,
    CONSTRAINT [PK_SubCategories] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [SuggestionMainCategories] (
    [Id] uniqueidentifier NOT NULL,
    [Title] nvarchar(max) NULL,
    [CategoryType] int NOT NULL,
    [Created] datetime2 NOT NULL,
    [Updated] datetime2 NOT NULL,
    CONSTRAINT [PK_SuggestionMainCategories] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [SuggestionSubCategories] (
    [Id] uniqueidentifier NOT NULL,
    [Title] nvarchar(max) NULL,
    [MainCategoryId] uniqueidentifier NOT NULL,
    [CategoryType] int NOT NULL,
    [Created] datetime2 NOT NULL,
    [Updated] datetime2 NOT NULL,
    CONSTRAINT [PK_SuggestionSubCategories] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [SwearWords] (
    [Id] uniqueidentifier NOT NULL,
    [Word] nvarchar(max) NULL,
    [Created] datetime2 NOT NULL,
    [Updated] datetime2 NOT NULL,
    CONSTRAINT [PK_SwearWords] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [User] (
    [Id] uniqueidentifier NOT NULL,
    [FirstName] nvarchar(max) NULL,
    [LastName] nvarchar(max) NULL,
    [PhoneNumber] nvarchar(max) NULL,
    [BirthDate] datetime2 NULL,
    [PhotoId] nvarchar(max) NULL,
    [Availability] bit NOT NULL,
    [PhoneVisibility] bit NOT NULL,
    [AllowCommunication] bit NOT NULL,
    [RequestSmsPermission] bit NOT NULL,
    [RatingScore] decimal(18,4) NULL,
    [Deleted] bit NOT NULL,
    [Discriminator] nvarchar(max) NOT NULL,
    [Profile_Id] uniqueidentifier NULL,
    [IsHidden] bit NULL,
    [CardNumber] nvarchar(max) NULL,
    [CertificateNo] nvarchar(max) NULL,
    [Created] datetime2 NOT NULL,
    [Updated] datetime2 NOT NULL,
    CONSTRAINT [PK_User] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Chat_Messages] (
    [Id] uniqueidentifier NOT NULL,
    [ChatId] uniqueidentifier NOT NULL,
    [SenderId] uniqueidentifier NULL,
    [IsSystem] bit NOT NULL,
    [Message] nvarchar(max) NULL,
    [MessageType] int NOT NULL,
    [SeenByCustomer] bit NOT NULL,
    [SeenByServiceman] bit NOT NULL,
    [IsContainProhibitedWord] bit NOT NULL,
    [Created] datetime2 NOT NULL,
    [Updated] datetime2 NOT NULL,
    CONSTRAINT [PK_Chat_Messages] PRIMARY KEY ([ChatId], [Id]),
    CONSTRAINT [FK_Chat_Messages_Chats_ChatId] FOREIGN KEY ([ChatId]) REFERENCES [Chats] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [Contracts] (
    [Id] uniqueidentifier NOT NULL,
    [ContractTypeId] uniqueidentifier NOT NULL,
    [Version] nvarchar(max) NULL,
    [ContractContent] nvarchar(max) NULL,
    [Deleted] bit NOT NULL,
    [Created] datetime2 NOT NULL,
    [Updated] datetime2 NOT NULL,
    CONSTRAINT [PK_Contracts] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Contracts_ContractTypes_ContractTypeId] FOREIGN KEY ([ContractTypeId]) REFERENCES [ContractTypes] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [Demands_PhotoUrls] (
    [PhotoId] uniqueidentifier NOT NULL,
    [DemandId] uniqueidentifier NOT NULL,
    [IsActive] bit NOT NULL,
    [PhotoUrl] nvarchar(max) NULL,
    CONSTRAINT [PK_Demands_PhotoUrls] PRIMARY KEY ([DemandId], [PhotoId]),
    CONSTRAINT [FK_Demands_PhotoUrls_Demands_DemandId] FOREIGN KEY ([DemandId]) REFERENCES [Demands] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [PrivateServiceman] (
    [DemandId] uniqueidentifier NOT NULL,
    [Id] int NOT NULL IDENTITY,
    [ServicemanId] uniqueidentifier NOT NULL,
    [RequestDate] datetime2 NOT NULL,
    CONSTRAINT [PK_PrivateServiceman] PRIMARY KEY ([DemandId], [Id]),
    CONSTRAINT [FK_PrivateServiceman_Demands_DemandId] FOREIGN KEY ([DemandId]) REFERENCES [Demands] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [ServicemanStatus] (
    [DemandId] uniqueidentifier NOT NULL,
    [Id] int NOT NULL IDENTITY,
    [ServicemanId] uniqueidentifier NOT NULL,
    [Status] tinyint NOT NULL,
    [RejectionReason_RejectionId] uniqueidentifier NULL,
    [RejectionReason_RejectionTitle] nvarchar(max) NULL,
    [RejectionReason_Detail] nvarchar(max) NULL,
    [CancelReason_CancelId] uniqueidentifier NULL,
    [CancelReason_Detail] nvarchar(max) NULL,
    CONSTRAINT [PK_ServicemanStatus] PRIMARY KEY ([DemandId], [Id]),
    CONSTRAINT [FK_ServicemanStatus_Demands_DemandId] FOREIGN KEY ([DemandId]) REFERENCES [Demands] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [Customer_ArchivedDemands] (
    [Id] uniqueidentifier NOT NULL,
    [CustomerId] uniqueidentifier NOT NULL,
    [DemandId] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_Customer_ArchivedDemands] PRIMARY KEY ([CustomerId], [Id]),
    CONSTRAINT [FK_Customer_ArchivedDemands_User_CustomerId] FOREIGN KEY ([CustomerId]) REFERENCES [User] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [Customer_BlockedServicemans] (
    [Id] uniqueidentifier NOT NULL,
    [CustomerId] uniqueidentifier NOT NULL,
    [ServicemanId] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_Customer_BlockedServicemans] PRIMARY KEY ([CustomerId], [Id]),
    CONSTRAINT [FK_Customer_BlockedServicemans_User_CustomerId] FOREIGN KEY ([CustomerId]) REFERENCES [User] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [Customer_FavoriteServicemans] (
    [Id] uniqueidentifier NOT NULL,
    [CustomerId] uniqueidentifier NOT NULL,
    [ServicemanId] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_Customer_FavoriteServicemans] PRIMARY KEY ([CustomerId], [Id]),
    CONSTRAINT [FK_Customer_FavoriteServicemans_User_CustomerId] FOREIGN KEY ([CustomerId]) REFERENCES [User] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [Customer_Location] (
    [CustomerId] uniqueidentifier NOT NULL,
    [CityId] uniqueidentifier NOT NULL,
    [CountyId] uniqueidentifier NOT NULL,
    [DistrictId] uniqueidentifier NULL,
    CONSTRAINT [PK_Customer_Location] PRIMARY KEY ([CustomerId]),
    CONSTRAINT [FK_Customer_Location_User_CustomerId] FOREIGN KEY ([CustomerId]) REFERENCES [User] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [OTPHistory] (
    [UserId] uniqueidentifier NOT NULL,
    [Id] int NOT NULL IDENTITY,
    [SentAt] datetime2 NOT NULL,
    CONSTRAINT [PK_OTPHistory] PRIMARY KEY ([UserId], [Id]),
    CONSTRAINT [FK_OTPHistory_User_UserId] FOREIGN KEY ([UserId]) REFERENCES [User] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [Serviceman_Certificates] (
    [Id] uniqueidentifier NOT NULL,
    [ProfileServicemanId] uniqueidentifier NOT NULL,
    [PhotoUrl] nvarchar(max) NULL,
    [TC] nvarchar(max) NULL,
    [ExpirationDate] datetime2 NOT NULL,
    [Status] int NOT NULL,
    CONSTRAINT [PK_Serviceman_Certificates] PRIMARY KEY ([ProfileServicemanId], [Id]),
    CONSTRAINT [FK_Serviceman_Certificates_User_ProfileServicemanId] FOREIGN KEY ([ProfileServicemanId]) REFERENCES [User] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [Serviceman_DemandStatuses] (
    [ServicemanId] uniqueidentifier NOT NULL,
    [Id] int NOT NULL IDENTITY,
    [DemandId] uniqueidentifier NOT NULL,
    [Status] tinyint NOT NULL,
    [IsArchived] bit NOT NULL,
    CONSTRAINT [PK_Serviceman_DemandStatuses] PRIMARY KEY ([ServicemanId], [Id]),
    CONSTRAINT [FK_Serviceman_DemandStatuses_User_ServicemanId] FOREIGN KEY ([ServicemanId]) REFERENCES [User] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [Serviceman_Location] (
    [ServicemanId] uniqueidentifier NOT NULL,
    [CityId] uniqueidentifier NOT NULL,
    [CountyId] uniqueidentifier NOT NULL,
    [DistrictId] uniqueidentifier NULL,
    CONSTRAINT [PK_Serviceman_Location] PRIMARY KEY ([ServicemanId]),
    CONSTRAINT [FK_Serviceman_Location_User_ServicemanId] FOREIGN KEY ([ServicemanId]) REFERENCES [User] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [Serviceman_MainCategories] (
    [Id] uniqueidentifier NOT NULL,
    [ProfileServicemanId] uniqueidentifier NOT NULL,
    [MainCategoryId] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_Serviceman_MainCategories] PRIMARY KEY ([ProfileServicemanId], [Id]),
    CONSTRAINT [FK_Serviceman_MainCategories_User_ProfileServicemanId] FOREIGN KEY ([ProfileServicemanId]) REFERENCES [User] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [Serviceman_Notifications] (
    [Id] uniqueidentifier NOT NULL,
    [ProfileServicemanId] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_Serviceman_Notifications] PRIMARY KEY ([ProfileServicemanId], [Id]),
    CONSTRAINT [FK_Serviceman_Notifications_User_ProfileServicemanId] FOREIGN KEY ([ProfileServicemanId]) REFERENCES [User] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [Serviceman_ReferenceJobs] (
    [Id] uniqueidentifier NOT NULL,
    [ProfileServicemanId] uniqueidentifier NOT NULL,
    [DemandId] uniqueidentifier NOT NULL,
    [Status] int NOT NULL,
    CONSTRAINT [PK_Serviceman_ReferenceJobs] PRIMARY KEY ([ProfileServicemanId], [Id]),
    CONSTRAINT [FK_Serviceman_ReferenceJobs_User_ProfileServicemanId] FOREIGN KEY ([ProfileServicemanId]) REFERENCES [User] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [Serviceman_ServiceLocations] (
    [Id] uniqueidentifier NOT NULL,
    [ProfileServicemanId] uniqueidentifier NOT NULL,
    [CityId] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_Serviceman_ServiceLocations] PRIMARY KEY ([ProfileServicemanId], [Id]),
    CONSTRAINT [FK_Serviceman_ServiceLocations_User_ProfileServicemanId] FOREIGN KEY ([ProfileServicemanId]) REFERENCES [User] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [Serviceman_StoreLocation] (
    [ProfileServicemanId] uniqueidentifier NOT NULL,
    [CityId] uniqueidentifier NOT NULL,
    [CountyId] uniqueidentifier NOT NULL,
    [DistrictId] uniqueidentifier NULL,
    CONSTRAINT [PK_Serviceman_StoreLocation] PRIMARY KEY ([ProfileServicemanId]),
    CONSTRAINT [FK_Serviceman_StoreLocation_User_ProfileServicemanId] FOREIGN KEY ([ProfileServicemanId]) REFERENCES [User] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [CustomerContracts] (
    [Id] uniqueidentifier NOT NULL,
    [ContractId] uniqueidentifier NOT NULL,
    [CustomerId] uniqueidentifier NOT NULL,
    [Deleted] bit NOT NULL,
    [Created] datetime2 NOT NULL,
    [Updated] datetime2 NOT NULL,
    CONSTRAINT [PK_CustomerContracts] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_CustomerContracts_Contracts_ContractId] FOREIGN KEY ([ContractId]) REFERENCES [Contracts] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_CustomerContracts_User_CustomerId] FOREIGN KEY ([CustomerId]) REFERENCES [User] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [ServicemanContracts] (
    [Id] uniqueidentifier NOT NULL,
    [ContractId] uniqueidentifier NOT NULL,
    [ServicemanId] uniqueidentifier NOT NULL,
    [Deleted] bit NOT NULL,
    [Created] datetime2 NOT NULL,
    [Updated] datetime2 NOT NULL,
    CONSTRAINT [PK_ServicemanContracts] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_ServicemanContracts_Contracts_ContractId] FOREIGN KEY ([ContractId]) REFERENCES [Contracts] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_ServicemanContracts_User_ServicemanId] FOREIGN KEY ([ServicemanId]) REFERENCES [User] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [Serviceman_SubCategories] (
    [Id] uniqueidentifier NOT NULL,
    [ServicemanMainCategoryProfileServicemanId] uniqueidentifier NOT NULL,
    [ServicemanMainCategoryId] uniqueidentifier NOT NULL,
    [SubCategoryId] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_Serviceman_SubCategories] PRIMARY KEY ([ServicemanMainCategoryProfileServicemanId], [ServicemanMainCategoryId], [Id]),
    CONSTRAINT [FK_Serviceman_SubCategories_Serviceman_MainCategories_ServicemanMainCategoryProfileServicemanId_ServicemanMainCategoryId] FOREIGN KEY ([ServicemanMainCategoryProfileServicemanId], [ServicemanMainCategoryId]) REFERENCES [Serviceman_MainCategories] ([ProfileServicemanId], [Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [Serviceman_ReferenceJobs_Photos] (
    [PhotoId] uniqueidentifier NOT NULL,
    [ReferenceJobProfileServicemanId] uniqueidentifier NOT NULL,
    [ReferenceJobId] uniqueidentifier NOT NULL,
    [IsActive] bit NOT NULL,
    [PhotoUrl] nvarchar(max) NULL,
    CONSTRAINT [PK_Serviceman_ReferenceJobs_Photos] PRIMARY KEY ([ReferenceJobProfileServicemanId], [ReferenceJobId], [PhotoId]),
    CONSTRAINT [FK_Serviceman_ReferenceJobs_Photos_Serviceman_ReferenceJobs_ReferenceJobProfileServicemanId_ReferenceJobId] FOREIGN KEY ([ReferenceJobProfileServicemanId], [ReferenceJobId]) REFERENCES [Serviceman_ReferenceJobs] ([ProfileServicemanId], [Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [Serviceman_ServiceLocations_Counties] (
    [Id] uniqueidentifier NOT NULL,
    [ServiceLocationProfileServicemanId] uniqueidentifier NOT NULL,
    [ServiceLocationId] uniqueidentifier NOT NULL,
    [CountyId] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_Serviceman_ServiceLocations_Counties] PRIMARY KEY ([ServiceLocationProfileServicemanId], [ServiceLocationId], [Id]),
    CONSTRAINT [FK_Serviceman_ServiceLocations_Counties_Serviceman_ServiceLocations_ServiceLocationProfileServicemanId_ServiceLocationId] FOREIGN KEY ([ServiceLocationProfileServicemanId], [ServiceLocationId]) REFERENCES [Serviceman_ServiceLocations] ([ProfileServicemanId], [Id]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_Contracts_ContractTypeId] ON [Contracts] ([ContractTypeId]);
GO

CREATE UNIQUE INDEX [IX_Contracts_Id] ON [Contracts] ([Id]);
GO

CREATE UNIQUE INDEX [IX_ContractTypes_Id] ON [ContractTypes] ([Id]);
GO

CREATE INDEX [IX_CustomerContracts_ContractId] ON [CustomerContracts] ([ContractId]);
GO

CREATE INDEX [IX_CustomerContracts_CustomerId] ON [CustomerContracts] ([CustomerId]);
GO

CREATE UNIQUE INDEX [IX_CustomerContracts_Id] ON [CustomerContracts] ([Id]);
GO

CREATE UNIQUE INDEX [IX_Demands_Id] ON [Demands] ([Id]);
GO

CREATE INDEX [IX_ServicemanContracts_ContractId] ON [ServicemanContracts] ([ContractId]);
GO

CREATE UNIQUE INDEX [IX_ServicemanContracts_Id] ON [ServicemanContracts] ([Id]);
GO

CREATE INDEX [IX_ServicemanContracts_ServicemanId] ON [ServicemanContracts] ([ServicemanId]);
GO

CREATE UNIQUE INDEX [IX_User_Id] ON [User] ([Id]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220422103748_Init', N'6.0.3');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [AppConfigs] ADD [AuthenticationKey] nvarchar(max) NULL;
GO

ALTER TABLE [AppConfigs] ADD [AuthorizeCode] nvarchar(max) NULL;
GO

ALTER TABLE [AppConfigs] ADD [BusinessCardBINCODE1] nvarchar(max) NULL;
GO

ALTER TABLE [AppConfigs] ADD [BusinessCardBINCODE2] nvarchar(max) NULL;
GO

ALTER TABLE [AppConfigs] ADD [BusinessCardBINCODE3] nvarchar(max) NULL;
GO

ALTER TABLE [AppConfigs] ADD [CommercialCardBINCODE1] nvarchar(max) NULL;
GO

ALTER TABLE [AppConfigs] ADD [CommercialCardBINCODE2] nvarchar(max) NULL;
GO

ALTER TABLE [AppConfigs] ADD [CommercialCardBINCODE3] nvarchar(max) NULL;
GO

ALTER TABLE [AppConfigs] ADD [POServicemanInquiriesUrl] nvarchar(max) NULL;
GO

ALTER TABLE [AppConfigs] ADD [Param1] nvarchar(max) NULL;
GO

ALTER TABLE [AppConfigs] ADD [ProcessType] nvarchar(max) NULL;
GO

ALTER TABLE [AppConfigs] ADD [QueryType] nvarchar(max) NULL;
GO

ALTER TABLE [AppConfigs] ADD [WorkplaceCode] nvarchar(max) NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220425105948_Added_ServicemanInquiries_AppConfigs', N'6.0.3');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [AppConfigs] ADD [POSurveyUrl] nvarchar(max) NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220425191150_Added_POSurveyUrl', N'6.0.3');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [AppConfigs] ADD [CheckSurveyProcessType] nvarchar(max) NULL;
GO

ALTER TABLE [AppConfigs] ADD [GetSurveyProcessType] nvarchar(max) NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220425201612_Added_Survey_Params', N'6.0.3');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [AppConfigs] ADD [POMemberCheckUrl] nvarchar(max) NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220426113357_Added_POMemberCheckUrl', N'6.0.3');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [AppConfigs] ADD [POMemberSegmentUpdateUrl] nvarchar(max) NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220426130844_Added_POMemberSegmentUpdateUrl', N'6.0.3');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

EXEC sp_rename N'[AppConfigs].[WorkplaceCode]', N'POServicemanInquiriesQueryType', N'COLUMN';
GO

EXEC sp_rename N'[AppConfigs].[QueryType]', N'POServicemanInquiriesProcessType', N'COLUMN';
GO

EXEC sp_rename N'[AppConfigs].[ProcessType]', N'POServicemanInquiriesParam1', N'COLUMN';
GO

EXEC sp_rename N'[AppConfigs].[Param1]', N'POMemberSegmentUpdateTC', N'COLUMN';
GO

EXEC sp_rename N'[AppConfigs].[AuthorizeCode]', N'POMemberSegmentUpdateSegmentInfo', N'COLUMN';
GO

ALTER TABLE [AppConfigs] ADD [CheckSurveyParam2] nvarchar(max) NULL;
GO

ALTER TABLE [AppConfigs] ADD [GetSurveyParam1] nvarchar(max) NULL;
GO

ALTER TABLE [AppConfigs] ADD [GetSurveyParam2] nvarchar(max) NULL;
GO

ALTER TABLE [AppConfigs] ADD [POMemberCheckParam1] nvarchar(max) NULL;
GO

ALTER TABLE [AppConfigs] ADD [POMemberCheckProcessType] nvarchar(max) NULL;
GO

ALTER TABLE [AppConfigs] ADD [POMemberSegmentUpdateAnketKod] nvarchar(max) NULL;
GO

ALTER TABLE [AppConfigs] ADD [POMemberSegmentUpdateBranchCode] nvarchar(max) NULL;
GO

ALTER TABLE [AppConfigs] ADD [POMemberSegmentUpdateHaveCertificate] nvarchar(max) NULL;
GO

ALTER TABLE [AppConfigs] ADD [POMemberSegmentUpdateHedefKitleKod] nvarchar(max) NULL;
GO

ALTER TABLE [AppConfigs] ADD [POMemberSegmentUpdateMagazaKod] nvarchar(max) NULL;
GO

ALTER TABLE [AppConfigs] ADD [POMemberSegmentUpdateProcessType] nvarchar(max) NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220426134842_Added_configs', N'6.0.3');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [AppConfigs] ADD [POSurveyInfoIsVerified] nvarchar(max) NULL;
GO

ALTER TABLE [AppConfigs] ADD [POSurveyInfoProcessType] nvarchar(max) NULL;
GO

ALTER TABLE [AppConfigs] ADD [POSurveyInfoSurveyCode] nvarchar(max) NULL;
GO

ALTER TABLE [AppConfigs] ADD [POSurveyInfoUrl] nvarchar(max) NULL;
GO

ALTER TABLE [AppConfigs] ADD [POSurveyInfoVerdeCode] nvarchar(max) NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220428104621_Added_SurveyInfoConfigs', N'6.0.3');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [AppConfigs] ADD [POVpiCommunicationProcessesBranchCode] nvarchar(max) NULL;
GO

ALTER TABLE [AppConfigs] ADD [POVpiCommunicationProcessesCustomerID] nvarchar(max) NULL;
GO

ALTER TABLE [AppConfigs] ADD [POVpiCommunicationProcessesFormBarcodeNo] nvarchar(max) NULL;
GO

ALTER TABLE [AppConfigs] ADD [POVpiCommunicationProcessesNotPreferredParoList] nvarchar(max) NULL;
GO

ALTER TABLE [AppConfigs] ADD [POVpiCommunicationProcessesParam1] nvarchar(max) NULL;
GO

ALTER TABLE [AppConfigs] ADD [POVpiCommunicationProcessesParoVpi] nvarchar(max) NULL;
GO

ALTER TABLE [AppConfigs] ADD [POVpiCommunicationProcessesProcessType] nvarchar(max) NULL;
GO

ALTER TABLE [AppConfigs] ADD [POVpiCommunicationProcessesSurveyCode] nvarchar(max) NULL;
GO

ALTER TABLE [AppConfigs] ADD [POVpiCommunicationProcessesUrl] nvarchar(max) NULL;
GO

ALTER TABLE [AppConfigs] ADD [POVpiCommunicationProcessesVpiVersion] nvarchar(max) NULL;
GO

ALTER TABLE [AppConfigs] ADD [POVpiCommunicationProcessesWorkplaceList] nvarchar(max) NULL;
GO

ALTER TABLE [AppConfigs] ADD [POVpiCommunicationProcessesWorkplaceVpi] nvarchar(max) NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220428120226_Added_VpiCommunicationProcessesConfigs', N'6.0.3');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [AppConfigs] ADD [POUpdateContactInfoBranchCode] nvarchar(max) NULL;
GO

ALTER TABLE [AppConfigs] ADD [POUpdateContactInfoCustomerID] nvarchar(max) NULL;
GO

ALTER TABLE [AppConfigs] ADD [POUpdateContactInfoDescription] nvarchar(max) NULL;
GO

ALTER TABLE [AppConfigs] ADD [POUpdateContactInfoDescriptionType] nvarchar(max) NULL;
GO

ALTER TABLE [AppConfigs] ADD [POUpdateContactInfoOptinIp] nvarchar(max) NULL;
GO

ALTER TABLE [AppConfigs] ADD [POUpdateContactInfoParam1] nvarchar(max) NULL;
GO

ALTER TABLE [AppConfigs] ADD [POUpdateContactInfoParam2] nvarchar(max) NULL;
GO

ALTER TABLE [AppConfigs] ADD [POUpdateContactInfoProcessType] nvarchar(max) NULL;
GO

ALTER TABLE [AppConfigs] ADD [POUpdateContactInfoUrl] nvarchar(max) NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220428123543_Added_POUpdateContactInfoConfigs', N'6.0.3');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [User] ADD [Status] int NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220428142531_Added_ServicemanStatus', N'6.0.3');
GO

COMMIT;
GO

