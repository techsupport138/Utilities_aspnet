SET IDENTITY_INSERT [dbo].[Country] ON 
GO
INSERT [dbo].[Country] ([CountryId], [CountryName]) VALUES (1, N'جمهوری اسلامی ایران')
GO
INSERT [dbo].[Country] ([CountryId], [CountryName]) VALUES (2, N'جمهوری اسلامی افقانستان')
GO
INSERT [dbo].[Country] ([CountryId], [CountryName]) VALUES (3, N'ژاپن')
GO
SET IDENTITY_INSERT [dbo].[Country] OFF
GO
SET IDENTITY_INSERT [dbo].[Province] ON 
GO
INSERT [dbo].[Province] ([ProvinceId], [CountryId], [ProvinceName]) VALUES (1, 1, N'آذربايجان شرقي')
GO
INSERT [dbo].[Province] ([ProvinceId], [CountryId], [ProvinceName]) VALUES (2, 1, N'آذربايجان غربي')
GO
INSERT [dbo].[Province] ([ProvinceId], [CountryId], [ProvinceName]) VALUES (3, 1, N'اردبيل')
GO
INSERT [dbo].[Province] ([ProvinceId], [CountryId], [ProvinceName]) VALUES (4, 1, N'اصفهان')
GO
INSERT [dbo].[Province] ([ProvinceId], [CountryId], [ProvinceName]) VALUES (5, 1, N'البرز')
GO
INSERT [dbo].[Province] ([ProvinceId], [CountryId], [ProvinceName]) VALUES (6, 1, N'ايلام')
GO
INSERT [dbo].[Province] ([ProvinceId], [CountryId], [ProvinceName]) VALUES (7, 1, N'بوشهر')
GO
INSERT [dbo].[Province] ([ProvinceId], [CountryId], [ProvinceName]) VALUES (8, 1, N'تهران')
GO
INSERT [dbo].[Province] ([ProvinceId], [CountryId], [ProvinceName]) VALUES (9, 1, N'چهارمحال وبختياري')
GO
INSERT [dbo].[Province] ([ProvinceId], [CountryId], [ProvinceName]) VALUES (10, 1, N'خراسان جنوبي')
GO
INSERT [dbo].[Province] ([ProvinceId], [CountryId], [ProvinceName]) VALUES (11, 1, N'خراسان رضوي')
GO
INSERT [dbo].[Province] ([ProvinceId], [CountryId], [ProvinceName]) VALUES (12, 1, N'خراسان شمالي')
GO
INSERT [dbo].[Province] ([ProvinceId], [CountryId], [ProvinceName]) VALUES (13, 1, N'خوزستان')
GO
INSERT [dbo].[Province] ([ProvinceId], [CountryId], [ProvinceName]) VALUES (14, 1, N'زنجان')
GO
INSERT [dbo].[Province] ([ProvinceId], [CountryId], [ProvinceName]) VALUES (15, 1, N'سمنان')
GO
INSERT [dbo].[Province] ([ProvinceId], [CountryId], [ProvinceName]) VALUES (16, 1, N'سيستان وبلوچستان')
GO
INSERT [dbo].[Province] ([ProvinceId], [CountryId], [ProvinceName]) VALUES (17, 1, N'فارس')
GO
INSERT [dbo].[Province] ([ProvinceId], [CountryId], [ProvinceName]) VALUES (18, 1, N'قزوين')
GO
INSERT [dbo].[Province] ([ProvinceId], [CountryId], [ProvinceName]) VALUES (19, 1, N'قم')
GO
INSERT [dbo].[Province] ([ProvinceId], [CountryId], [ProvinceName]) VALUES (20, 1, N'کردستان')
GO
INSERT [dbo].[Province] ([ProvinceId], [CountryId], [ProvinceName]) VALUES (21, 1, N'کرمان')
GO
INSERT [dbo].[Province] ([ProvinceId], [CountryId], [ProvinceName]) VALUES (22, 1, N'کرمانشاه')
GO
INSERT [dbo].[Province] ([ProvinceId], [CountryId], [ProvinceName]) VALUES (23, 1, N'کهگيلويه وبويراحمد')
GO
INSERT [dbo].[Province] ([ProvinceId], [CountryId], [ProvinceName]) VALUES (24, 1, N'گلستان')
GO
INSERT [dbo].[Province] ([ProvinceId], [CountryId], [ProvinceName]) VALUES (25, 1, N'گيلان')
GO
INSERT [dbo].[Province] ([ProvinceId], [CountryId], [ProvinceName]) VALUES (26, 1, N'لرستان')
GO
INSERT [dbo].[Province] ([ProvinceId], [CountryId], [ProvinceName]) VALUES (27, 1, N'مازندران')
GO
INSERT [dbo].[Province] ([ProvinceId], [CountryId], [ProvinceName]) VALUES (28, 1, N'مرکزي')
GO
INSERT [dbo].[Province] ([ProvinceId], [CountryId], [ProvinceName]) VALUES (29, 1, N'هرمزگان')
GO
INSERT [dbo].[Province] ([ProvinceId], [CountryId], [ProvinceName]) VALUES (30, 1, N'همدان')
GO
INSERT [dbo].[Province] ([ProvinceId], [CountryId], [ProvinceName]) VALUES (31, 1, N'يزد')
GO
INSERT [dbo].[Province] ([ProvinceId], [CountryId], [ProvinceName]) VALUES (100, 1, N'اتباع خارجی')
GO
SET IDENTITY_INSERT [dbo].[Province] OFF
GO
SET IDENTITY_INSERT [dbo].[City] ON 
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (1, 1, N'آذرشهر')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (2, 1, N'تيمورلو')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (3, 1, N'گوگان')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (4, 1, N'ممقان')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (5, 1, N'اسکو')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (6, 1, N'ايلخچي')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (7, 1, N'سهند')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (8, 1, N'اهر')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (9, 1, N'هوراند')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (10, 1, N'بستان آباد')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (11, 1, N'تيکمه داش')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (12, 1, N'بناب')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (13, 1, N'باسمنج')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (14, 1, N'تبريز')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (15, 1, N'خسروشاه')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (16, 1, N'سردرود')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (17, 1, N'جلفا')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (18, 1, N'سيه رود')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (19, 1, N'هاديشهر')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (20, 1, N'قره آغاج')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (21, 1, N'خمارلو')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (22, 1, N'دوزدوزان')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (23, 1, N'سراب')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (24, 1, N'شربيان')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (25, 1, N'مهربان')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (26, 1, N'تسوج')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (27, 1, N'خامنه')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (28, 1, N'سيس')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (29, 1, N'شبستر')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (30, 1, N'شرفخانه')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (31, 1, N'شندآباد')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (32, 1, N'صوفيان')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (33, 1, N'کوزه کنان')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (34, 1, N'وايقان')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (35, 1, N'جوان قلعه')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (36, 1, N'عجب شير')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (37, 1, N'آبش احمد')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (38, 1, N'کليبر')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (39, 1, N'خداجو(خراجو)')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (40, 1, N'مراغه')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (41, 1, N'بناب مرند')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (42, 1, N'زنوز')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (43, 1, N'کشکسراي')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (44, 1, N'مرند')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (45, 1, N'يامچي')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (46, 1, N'ليلان')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (47, 1, N'مبارک شهر')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (48, 1, N'ملکان')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (49, 1, N'آقکند')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (50, 1, N'اچاچي')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (51, 1, N'ترک')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (52, 1, N'ترکمانچاي')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (53, 1, N'ميانه')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (54, 1, N'خاروانا')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (55, 1, N'ورزقان')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (56, 1, N'بخشايش')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (57, 1, N'خواجه')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (58, 1, N'زرنق')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (59, 1, N'کلوانق')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (60, 1, N'هريس')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (61, 1, N'نظرکهريزي')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (62, 1, N'هشترود')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (63, 2, N'اروميه')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (64, 2, N'سرو')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (65, 2, N'سيلوانه')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (66, 2, N'قوشچي')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (67, 2, N'نوشين')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (68, 2, N'اشنويه')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (69, 2, N'نالوس')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (70, 2, N'بوکان')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (71, 2, N'سيمينه')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (72, 2, N'پلدشت')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (73, 2, N'نازک عليا')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (74, 2, N'پيرانشهر')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (75, 2, N'گردکشانه')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (76, 2, N'تکاب')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (77, 2, N'آواجيق')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (78, 2, N'سيه چشمه')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (79, 2, N'قره ضياءالدين')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (80, 2, N'ايواوغلي')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (81, 2, N'خوي')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (82, 2, N'ديزج ديز')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (83, 2, N'زرآباد')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (84, 2, N'فيرورق')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (85, 2, N'قطور')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (86, 2, N'ربط')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (87, 2, N'سردشت')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (88, 2, N'ميرآباد')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (89, 2, N'تازه شهر')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (90, 2, N'سلماس')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (91, 2, N'شاهين دژ')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (92, 2, N'کشاورز')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (93, 2, N'محمودآباد')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (94, 2, N'شوط')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (95, 2, N'مرگنلر')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (96, 2, N'بازرگان')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (97, 2, N'ماکو')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (98, 2, N'خليفان')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (99, 2, N'مهاباد')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (100, 2, N'باروق')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (101, 2, N'چهاربرج')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (102, 2, N'مياندوآب')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (103, 2, N'محمديار')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (104, 2, N'نقده')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (105, 3, N'اردبيل')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (106, 3, N'هير')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (107, 3, N'بيله سوار')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (108, 3, N'جعفرآباد')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (109, 3, N'اسلام اباد')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (110, 3, N'اصلاندوز')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (111, 3, N'پارس آباد')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (112, 3, N'تازه کند')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (113, 3, N'خلخال')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (114, 3, N'کلور')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (115, 3, N'هشتجين')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (116, 3, N'سرعين')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (117, 3, N'گيوي')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (118, 3, N'تازه کندانگوت')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (119, 3, N'گرمي')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (120, 3, N'رضي')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (121, 3, N'فخراباد')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (122, 3, N'قصابه')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (123, 3, N'لاهرود')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (124, 3, N'مرادلو')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (125, 3, N'مشگين شهر')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (126, 3, N'آبي بيگلو')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (127, 3, N'عنبران')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (128, 3, N'نمين')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (129, 3, N'کوراييم')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (130, 3, N'نير')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (131, 4, N'آران وبيدگل')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (132, 4, N'ابوزيدآباد')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (133, 4, N'سفيدشهر')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (134, 4, N'نوش آباد')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (135, 4, N'اردستان')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (136, 4, N'زواره')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (137, 4, N'مهاباد')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (138, 4, N'اژيه')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (139, 4, N'اصفهان')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (140, 4, N'بهارستان')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (141, 4, N'تودشک')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (142, 4, N'حسن اباد')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (143, 4, N'زيار')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (144, 4, N'سجزي')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (145, 4, N'قهجاورستان')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (146, 4, N'کوهپايه')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (147, 4, N'محمدآباد')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (148, 4, N'نصرآباد')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (149, 4, N'نيک آباد')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (150, 4, N'ورزنه')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (151, 4, N'هرند')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (152, 4, N'حبيب آباد')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (153, 4, N'خورزوق')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (154, 4, N'دستگرد')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (155, 4, N'دولت آباد')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (156, 4, N'سين')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (157, 4, N'شاپورآباد')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (158, 4, N'کمشچه')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (159, 4, N'افوس')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (160, 4, N'بويين ومياندشت')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (161, 4, N'تيران')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (162, 4, N'رضوانشهر')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (163, 4, N'عسگران')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (164, 4, N'چادگان')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (165, 4, N'رزوه')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (166, 4, N'اصغرآباد')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (167, 4, N'خميني شهر')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (168, 4, N'درچه')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (169, 4, N'کوشک')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (170, 4, N'خوانسار')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (171, 4, N'جندق')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (172, 4, N'خور')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (173, 4, N'فرخي')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (174, 4, N'دهاقان')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (175, 4, N'گلشن')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (176, 4, N'حنا')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (177, 4, N'سميرم')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (178, 4, N'کمه')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (179, 4, N'ونک')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (180, 4, N'شاهين شهر')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (181, 4, N'گرگاب')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (182, 4, N'گزبرخوار')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (183, 4, N'لاي بيد')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (184, 4, N'ميمه')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (185, 4, N'وزوان')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (186, 4, N'شهرضا')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (187, 4, N'منظريه')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (188, 4, N'داران')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (189, 4, N'دامنه')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (190, 4, N'برف انبار')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (191, 4, N'فريدونشهر')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (192, 4, N'ابريشم')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (193, 4, N'ايمانشهر')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (194, 4, N'بهاران شهر')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (195, 4, N'پيربکران')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (196, 4, N'زازران')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (197, 4, N'فلاورجان')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (198, 4, N'قهدريجان')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (199, 4, N'کليشادوسودرجان')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (200, 4, N'برزک')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (201, 4, N'جوشقان قالي')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (202, 4, N'قمصر')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (203, 4, N'کاشان')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (204, 4, N'کامو و چوگان')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (205, 4, N'مشکات')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (206, 4, N'نياسر')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (207, 4, N'گلپايگان')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (208, 4, N'گلشهر')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (209, 4, N'گوگد')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (210, 4, N'باغ بهادران')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (211, 4, N'باغشاد')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (212, 4, N'چرمهين')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (213, 4, N'چمگردان')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (214, 4, N'زاينده رود')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (215, 4, N'زرين شهر')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (216, 4, N'سده لنجان')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (217, 4, N'فولادشهر')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (218, 4, N'ورنامخواست')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (219, 4, N'ديزيچه')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (220, 4, N'زيباشهر')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (221, 4, N'طالخونچه')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (222, 4, N'کرکوند')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (223, 4, N'مبارکه')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (224, 4, N'مجلسي')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (225, 4, N'انارک')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (226, 4, N'بافران')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (227, 4, N'نايين')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (228, 4, N'جوزدان')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (229, 4, N'دهق')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (230, 4, N'علويجه')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (231, 4, N'کهريزسنگ')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (232, 4, N'گلدشت')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (233, 4, N'نجف آباد')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (234, 4, N'بادرود')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (235, 4, N'خالدآباد')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (236, 4, N'طرق رود')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (237, 4, N'نطنز')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (238, 5, N'اشتهارد')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (239, 5, N'چهارباغ')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (240, 5, N'شهرجديدهشتگرد')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (241, 5, N'کوهسار')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (242, 5, N'گلسار')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (243, 5, N'هشتگرد')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (244, 5, N'طالقان')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (245, 5, N'فرديس')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (246, 5, N'مشکين دشت')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (247, 5, N'آسارا')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (248, 5, N'کرج')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (249, 5, N'کمال شهر')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (250, 5, N'گرمدره')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (251, 5, N'ماهدشت')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (252, 5, N'محمدشهر')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (253, 5, N'تنکمان')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (254, 5, N'نظرآباد')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (255, 6, N'آبدانان')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (256, 6, N'سراب باغ')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (257, 6, N'مورموري')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (258, 6, N'ايلام')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (259, 6, N'چوار')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (260, 6, N'ايوان')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (261, 6, N'زرنه')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (262, 6, N'بدره')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (263, 6, N'آسمان آباد')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (264, 6, N'بلاوه')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (265, 6, N'توحيد')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (266, 6, N'سرابله')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (267, 6, N'شباب')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (268, 6, N'دره شهر')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (269, 6, N'ماژين')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (270, 6, N'پهله')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (271, 6, N'دهلران')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (272, 6, N'موسيان')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (273, 6, N'ميمه')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (274, 6, N'لومار')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (275, 6, N'ارکواز')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (276, 6, N'دلگشا')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (277, 6, N'مهر')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (278, 6, N'صالح آباد')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (279, 6, N'مهران')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (280, 7, N'بوشهر')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (281, 7, N'چغادک')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (282, 7, N'خارک')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (283, 7, N'عالي شهر')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (284, 7, N'آباد')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (285, 7, N'اهرم')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (286, 7, N'دلوار')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (287, 7, N'انارستان')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (288, 7, N'جم')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (289, 7, N'ريز')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (290, 7, N'آب پخش')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (291, 7, N'برازجان')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (292, 7, N'بوشکان')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (293, 7, N'تنگ ارم')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (294, 7, N'دالکي')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (295, 7, N'سعد آباد')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (296, 7, N'شبانکاره')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (297, 7, N'کلمه')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (298, 7, N'وحدتيه')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (299, 7, N'بادوله')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (300, 7, N'خورموج')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (301, 7, N'شنبه')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (302, 7, N'کاکي')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (303, 7, N'آبدان')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (304, 7, N'بردخون')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (305, 7, N'بردستان')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (306, 7, N'بندردير')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (307, 7, N'دوراهک')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (308, 7, N'امام حسن')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (309, 7, N'بندرديلم')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (310, 7, N'عسلويه')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (311, 7, N'نخل تقي')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (312, 7, N'بندرکنگان')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (313, 7, N'بنک')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (314, 7, N'سيراف')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (315, 7, N'بندرريگ')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (316, 7, N'بندرگناوه')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (317, 8, N'احمد آباد مستوفي')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (318, 8, N'اسلامشهر')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (319, 8, N'چهاردانگه')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (320, 8, N'صالحيه')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (321, 8, N'گلستان')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (322, 8, N'نسيم شهر')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (323, 8, N'پاکدشت')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (324, 8, N'شريف آباد')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (325, 8, N'فرون اباد')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (326, 8, N'بومهن')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (327, 8, N'پرديس')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (328, 8, N'پيشوا')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (329, 8, N'تهران')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (330, 8, N'آبسرد')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (331, 8, N'آبعلي')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (332, 8, N'دماوند')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (333, 8, N'رودهن')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (334, 8, N'کيلان')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (335, 8, N'پرند')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (336, 8, N'رباطکريم')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (337, 8, N'نصيرشهر')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (338, 8, N'باقرشهر')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (339, 8, N'حسن آباد')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (340, 8, N'ري')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (341, 8, N'کهريزک')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (342, 8, N'تجريش')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (343, 8, N'شمشک')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (344, 8, N'فشم')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (345, 8, N'لواسان')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (346, 8, N'انديشه')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (347, 8, N'باغستان')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (348, 8, N'شاهدشهر')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (349, 8, N'شهريار')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (350, 8, N'صباشهر')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (351, 8, N'فردوسيه')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (352, 8, N'وحيديه')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (353, 8, N'ارجمند')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (354, 8, N'فيروزکوه')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (355, 8, N'قدس')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (356, 8, N'قرچک')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (357, 8, N'صفادشت')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (358, 8, N'ملارد')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (359, 8, N'جوادآباد')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (360, 8, N'ورامين')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (361, 9, N'اردل')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (362, 9, N'دشتک')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (363, 9, N'سرخون')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (364, 9, N'کاج')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (365, 9, N'بروجن')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (366, 9, N'بلداجي')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (367, 9, N'سفيددشت')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (368, 9, N'فرادبنه')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (369, 9, N'گندمان')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (370, 9, N'نقنه')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (371, 9, N'بن')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (372, 9, N'وردنجان')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (373, 9, N'سامان')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (374, 9, N'سودجان')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (375, 9, N'سورشجان')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (376, 9, N'شهرکرد')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (377, 9, N'طاقانک')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (378, 9, N'فرخ شهر')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (379, 9, N'کيان')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (380, 9, N'نافچ')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (381, 9, N'هاروني')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (382, 9, N'هفشجان')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (383, 9, N'باباحيدر')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (384, 9, N'پردنجان')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (385, 9, N'جونقان')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (386, 9, N'چليچه')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (387, 9, N'فارسان')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (388, 9, N'گوجان')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (389, 9, N'بازفت')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (390, 9, N'چلگرد')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (391, 9, N'صمصامي')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (392, 9, N'دستنا')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (393, 9, N'شلمزار')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (394, 9, N'گهرو')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (395, 9, N'ناغان')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (396, 9, N'آلوني')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (397, 9, N'سردشت')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (398, 9, N'لردگان')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (399, 9, N'مال خليفه')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (400, 9, N'منج')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (401, 10, N'ارسک')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (402, 10, N'بشرويه')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (403, 10, N'بيرجند')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (404, 10, N'خوسف')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (405, 10, N'محمدشهر')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (406, 10, N'اسديه')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (407, 10, N'طبس مسينا')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (408, 10, N'قهستان')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (409, 10, N'گزيک')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (410, 10, N'حاجي آباد')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (411, 10, N'زهان')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (412, 10, N'آيسک')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (413, 10, N'سرايان')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (414, 10, N'سه قلعه')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (415, 10, N'سربيشه')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (416, 10, N'مود')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (417, 10, N'ديهوک')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (418, 10, N'طبس')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (419, 10, N'عشق آباد')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (420, 10, N'اسلاميه')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (421, 10, N'فردوس')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (422, 10, N'آرين شهر')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (423, 10, N'اسفدن')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (424, 10, N'خضري دشت بياض')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (425, 10, N'قاين')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (426, 10, N'نيمبلوک')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (427, 10, N'شوسف')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (428, 10, N'نهبندان')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (429, 11, N'باخرز')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (430, 11, N'بجستان')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (431, 11, N'يونسي')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (432, 11, N'انابد')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (433, 11, N'بردسکن')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (434, 11, N'شهراباد')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (435, 11, N'شانديز')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (436, 11, N'طرقبه')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (437, 11, N'تايباد')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (438, 11, N'کاريز')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (439, 11, N'مشهدريزه')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (440, 11, N'احمدابادصولت')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (441, 11, N'تربت جام')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (442, 11, N'صالح آباد')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (443, 11, N'نصرآباد')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (444, 11, N'نيل شهر')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (445, 11, N'بايک')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (446, 11, N'تربت حيدريه')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (447, 11, N'رباط سنگ')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (448, 11, N'کدکن')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (449, 11, N'جغتاي')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (450, 11, N'نقاب')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (451, 11, N'چناران')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (452, 11, N'گلبهار')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (453, 11, N'گلمکان')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (454, 11, N'خليل آباد')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (455, 11, N'کندر')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (456, 11, N'خواف')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (457, 11, N'سلامي')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (458, 11, N'سنگان')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (459, 11, N'قاسم آباد')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (460, 11, N'نشتيفان')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (461, 11, N'سلطان آباد')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (462, 11, N'داورزن')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (463, 11, N'چاپشلو')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (464, 11, N'درگز')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (465, 11, N'لطف آباد')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (466, 11, N'نوخندان')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (467, 11, N'جنگل')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (468, 11, N'رشتخوار')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (469, 11, N'دولت آباد')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (470, 11, N'روداب')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (471, 11, N'سبزوار')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (472, 11, N'ششتمد')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (473, 11, N'سرخس')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (474, 11, N'مزدآوند')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (475, 11, N'سفيدسنگ')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (476, 11, N'فرهادگرد')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (477, 11, N'فريمان')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (478, 11, N'قلندرآباد')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (479, 11, N'فيروزه')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (480, 11, N'همت آباد')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (481, 11, N'باجگيران')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (482, 11, N'قوچان')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (483, 11, N'ريوش')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (484, 11, N'کاشمر')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (485, 11, N'شهرزو')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (486, 11, N'کلات')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (487, 11, N'بيدخت')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (488, 11, N'کاخک')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (489, 11, N'گناباد')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (490, 11, N'رضويه')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (491, 11, N'مشهد')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (492, 11, N'مشهد ثامن')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (493, 11, N'ملک آباد')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (494, 11, N'شادمهر')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (495, 11, N'فيض آباد')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (496, 11, N'بار')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (497, 11, N'چکنه')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (498, 11, N'خرو')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (499, 11, N'درود')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (500, 11, N'عشق آباد')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (501, 11, N'قدمگاه')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (502, 11, N'نيشابور')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (503, 12, N'اسفراين')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (504, 12, N'صفي آباد')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (505, 12, N'بجنورد')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (506, 12, N'چناران شهر')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (507, 12, N'حصارگرمخان')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (508, 12, N'جاجرم')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (509, 12, N'سنخواست')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (510, 12, N'شوقان')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (511, 12, N'راز')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (512, 12, N'زيارت')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (513, 12, N'شيروان')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (514, 12, N'قوشخانه')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (515, 12, N'لوجلي')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (516, 12, N'تيتکانلو')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (517, 12, N'فاروج')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (518, 12, N'ايور')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (519, 12, N'درق')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (520, 12, N'گرمه')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (521, 12, N'آشخانه')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (522, 12, N'آوا')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (523, 12, N'پيش قلعه')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (524, 12, N'قاضي')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (525, 13, N'آبادان')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (526, 13, N'اروندکنار')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (527, 13, N'چويبده')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (528, 13, N'آغاجاري')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (529, 13, N'اميديه')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (530, 13, N'جايزان')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (531, 13, N'آبژدان')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (532, 13, N'قلعه خواجه')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (533, 13, N'آزادي')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (534, 13, N'انديمشک')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (535, 13, N'بيدروبه')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (536, 13, N'چم گلک')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (537, 13, N'حسينيه')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (538, 13, N'الهايي')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (539, 13, N'اهواز')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (540, 13, N'ايذه')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (541, 13, N'دهدز')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (542, 13, N'باغ ملک')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (543, 13, N'صيدون')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (544, 13, N'قلعه تل')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (545, 13, N'ميداود')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (546, 13, N'شيبان')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (547, 13, N'ملاثاني')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (548, 13, N'ويس')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (549, 13, N'بندرامام خميني')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (550, 13, N'بندرماهشهر')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (551, 13, N'چمران')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (552, 13, N'بهبهان')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (553, 13, N'تشان')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (554, 13, N'سردشت')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (555, 13, N'منصوريه')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (556, 13, N'حميديه')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (557, 13, N'خرمشهر')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (558, 13, N'مقاومت')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (559, 13, N'مينوشهر')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (560, 13, N'چغاميش')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (561, 13, N'حمزه')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (562, 13, N'دزفول')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (563, 13, N'سالند')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (564, 13, N'سياه منصور')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (565, 13, N'شمس آباد')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (566, 13, N'شهر امام')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (567, 13, N'صفي آباد')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (568, 13, N'ميانرود')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (569, 13, N'ابوحميظه')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (570, 13, N'بستان')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (571, 13, N'سوسنگرد')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (572, 13, N'کوت سيدنعيم')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (573, 13, N'رامشير')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (574, 13, N'مشراگه')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (575, 13, N'رامهرمز')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (576, 13, N'خنافره')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (577, 13, N'دارخوين')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (578, 13, N'شادگان')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (579, 13, N'الوان')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (580, 13, N'حر')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (581, 13, N'شاوور')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (582, 13, N'شوش')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (583, 13, N'فتح المبين')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (584, 13, N'سرداران')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (585, 13, N'شرافت')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (586, 13, N'شوشتر')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (587, 13, N'گوريه')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (588, 13, N'کوت عبداله')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (589, 13, N'ترکالکي')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (590, 13, N'جنت مکان')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (591, 13, N'سماله')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (592, 13, N'صالح شهر')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (593, 13, N'گتوند')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (594, 13, N'لالي')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (595, 13, N'گلگير')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (596, 13, N'مسجدسليمان')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (597, 13, N'هفتگل')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (598, 13, N'زهره')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (599, 13, N'هنديجان')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (600, 13, N'رفيع')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (601, 13, N'هويزه')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (602, 14, N'ابهر')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (603, 14, N'صايين قلعه')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (604, 14, N'هيدج')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (605, 14, N'حلب')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (606, 14, N'زرين آباد')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (607, 14, N'زرين رود')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (608, 14, N'سجاس')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (609, 14, N'سهرورد')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (610, 14, N'قيدار')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (611, 14, N'کرسف')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (612, 14, N'گرماب')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (613, 14, N'نوربهار')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (614, 14, N'خرمدره')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (615, 14, N'ارمغانخانه')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (616, 14, N'زنجان')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (617, 14, N'نيک پي')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (618, 14, N'سلطانيه')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (619, 14, N'آب بر')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (620, 14, N'چورزق')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (621, 14, N'دندي')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (622, 14, N'ماه نشان')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (623, 15, N'آرادان')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (624, 15, N'کهن آباد')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (625, 15, N'اميريه')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (626, 15, N'دامغان')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (627, 15, N'ديباج')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (628, 15, N'کلاته')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (629, 15, N'سرخه')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (630, 15, N'سمنان')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (631, 15, N'بسطام')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (632, 15, N'بيارجمند')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (633, 15, N'روديان')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (634, 15, N'شاهرود')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (635, 15, N'کلاته خيج')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (636, 15, N'مجن')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (637, 15, N'ايوانکي')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (638, 15, N'گرمسار')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (639, 15, N'درجزين')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (640, 15, N'شهميرزاد')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (641, 15, N'مهدي شهر')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (642, 15, N'ميامي')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (643, 16, N'ايرانشهر')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (644, 16, N'بزمان')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (645, 16, N'بمپور')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (646, 16, N'محمدان')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (647, 16, N'چابهار')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (648, 16, N'نگور')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (649, 16, N'خاش')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (650, 16, N'نوک آباد')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (651, 16, N'گلمورتي')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (652, 16, N'بنجار')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (653, 16, N'زابل')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (654, 16, N'زاهدان')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (655, 16, N'نصرت آباد')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (656, 16, N'زهک')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (657, 16, N'جالق')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (658, 16, N'سراوان')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (659, 16, N'سيرکان')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (660, 16, N'گشت')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (661, 16, N'محمدي')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (662, 16, N'پيشين')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (663, 16, N'راسک')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (664, 16, N'سرباز')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (665, 16, N'سوران')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (666, 16, N'هيدوچ')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (667, 16, N'فنوج')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (668, 16, N'قصرقند')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (669, 16, N'زرآباد')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (670, 16, N'کنارک')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (671, 16, N'مهرستان')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (672, 16, N'ميرجاوه')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (673, 16, N'اسپکه')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (674, 16, N'بنت')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (675, 16, N'نيک شهر')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (676, 16, N'اديمي')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (677, 16, N'شهرک علي اکبر')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (678, 16, N'محمدآباد')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (679, 16, N'دوست محمد')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (680, 17, N'آباده')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (681, 17, N'ايزدخواست')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (682, 17, N'بهمن')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (683, 17, N'سورمق')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (684, 17, N'صغاد')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (685, 17, N'ارسنجان')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (686, 17, N'استهبان')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (687, 17, N'ايج')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (688, 17, N'رونيز')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (689, 17, N'اقليد')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (690, 17, N'حسن اباد')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (691, 17, N'دژکرد')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (692, 17, N'سده')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (693, 17, N'بوانات')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (694, 17, N'حسامي')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (695, 17, N'کره اي')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (696, 17, N'مزايجان')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (697, 17, N'سعادت شهر')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (698, 17, N'مادرسليمان')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (699, 17, N'باب انار')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (700, 17, N'جهرم')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (701, 17, N'خاوران')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (702, 17, N'دوزه')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (703, 17, N'قطب آباد')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (704, 17, N'خرامه')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (705, 17, N'سلطان شهر')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (706, 17, N'صفاشهر')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (707, 17, N'قادراباد')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (708, 17, N'خنج')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (709, 17, N'جنت شهر')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (710, 17, N'داراب')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (711, 17, N'دوبرجي')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (712, 17, N'فدامي')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (713, 17, N'کوپن')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (714, 17, N'مصيري')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (715, 17, N'حاجي آباد')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (716, 17, N'دبيران')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (717, 17, N'شهرپير')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (718, 17, N'اردکان')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (719, 17, N'بيضا')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (720, 17, N'هماشهر')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (721, 17, N'سروستان')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (722, 17, N'کوهنجان')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (723, 17, N'خانه زنيان')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (724, 17, N'داريان')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (725, 17, N'زرقان')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (726, 17, N'شهرصدرا')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (727, 17, N'شيراز')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (728, 17, N'لپويي')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (729, 17, N'دهرم')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (730, 17, N'فراشبند')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (731, 17, N'نوجين')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (732, 17, N'زاهدشهر')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (733, 17, N'ششده')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (734, 17, N'فسا')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (735, 17, N'قره بلاغ')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (736, 17, N'ميانشهر')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (737, 17, N'نوبندگان')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (738, 17, N'فيروزآباد')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (739, 17, N'ميمند')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (740, 17, N'افزر')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (741, 17, N'امام شهر')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (742, 17, N'قير')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (743, 17, N'کارزين (فتح آباد)')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (744, 17, N'مبارک آبادديز')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (745, 17, N'بالاده')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (746, 17, N'خشت')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (747, 17, N'قايميه')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (748, 17, N'کازرون')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (749, 17, N'کنارتخته')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (750, 17, N'نودان')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (751, 17, N'کوار')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (752, 17, N'گراش')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (753, 17, N'اوز')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (754, 17, N'بنارويه')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (755, 17, N'بيرم')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (756, 17, N'جويم')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (757, 17, N'خور')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (758, 17, N'عمادده')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (759, 17, N'لار')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (760, 17, N'لطيفي')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (761, 17, N'اشکنان')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (762, 17, N'اهل')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (763, 17, N'علامرودشت')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (764, 17, N'لامرد')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (765, 17, N'خانيمن')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (766, 17, N'رامجرد')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (767, 17, N'سيدان')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (768, 17, N'کامفيروز')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (769, 17, N'مرودشت')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (770, 17, N'بابامنير')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (771, 17, N'خومه زار')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (772, 17, N'نورآباد')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (773, 17, N'اسير')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (774, 17, N'خوزي')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (775, 17, N'گله دار')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (776, 17, N'مهر')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (777, 17, N'وراوي')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (778, 17, N'آباده طشک')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (779, 17, N'قطرويه')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (780, 17, N'مشکان')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (781, 17, N'ني ريز')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (782, 18, N'آبيک')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (783, 18, N'خاکعلي')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (784, 18, N'آبگرم')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (785, 18, N'آوج')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (786, 18, N'الوند')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (787, 18, N'بيدستان')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (788, 18, N'شريفيه')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (789, 18, N'محمديه')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (790, 18, N'ارداق')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (791, 18, N'بويين زهرا')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (792, 18, N'دانسفهان')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (793, 18, N'سگزآباد')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (794, 18, N'شال')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (795, 18, N'اسفرورين')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (796, 18, N'تاکستان')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (797, 18, N'خرمدشت')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (798, 18, N'ضياڈآباد')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (799, 18, N'نرجه')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (800, 18, N'اقباليه')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (801, 18, N'رازميان')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (802, 18, N'سيردان')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (803, 18, N'قزوين')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (804, 18, N'کوهين')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (805, 18, N'محمودآبادنمونه')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (806, 18, N'معلم کلايه')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (807, 19, N'جعفريه')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (808, 19, N'دستجرد')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (809, 19, N'سلفچگان')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (810, 19, N'قم')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (811, 19, N'قنوات')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (812, 19, N'کهک')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (813, 20, N'آرمرده')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (814, 20, N'بانه')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (815, 20, N'بويين سفلي')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (816, 20, N'کاني سور')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (817, 20, N'بابارشاني')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (818, 20, N'بيجار')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (819, 20, N'پيرتاج')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (820, 20, N'توپ آغاج')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (821, 20, N'ياسوکند')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (822, 20, N'بلبان آباد')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (823, 20, N'دهگلان')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (824, 20, N'ديواندره')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (825, 20, N'زرينه')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (826, 20, N'اورامان تخت')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (827, 20, N'سروآباد')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (828, 20, N'سقز')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (829, 20, N'صاحب')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (830, 20, N'سنندج')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (831, 20, N'شويشه')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (832, 20, N'دزج')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (833, 20, N'دلبران')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (834, 20, N'سريش آباد')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (835, 20, N'قروه')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (836, 20, N'کامياران')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (837, 20, N'موچش')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (838, 20, N'برده رشه')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (839, 20, N'چناره')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (840, 20, N'کاني دينار')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (841, 20, N'مريوان')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (842, 21, N'ارزوييه')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (843, 21, N'امين شهر')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (844, 21, N'انار')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (845, 21, N'بافت')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (846, 21, N'بزنجان')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (847, 21, N'بردسير')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (848, 21, N'دشتکار')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (849, 21, N'گلزار')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (850, 21, N'لاله زار')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (851, 21, N'نگار')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (852, 21, N'بروات')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (853, 21, N'بم')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (854, 21, N'بلوک')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (855, 21, N'جبالبارز')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (856, 21, N'جيرفت')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (857, 21, N'درب بهشت')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (858, 21, N'رابر')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (859, 21, N'هنزا')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (860, 21, N'راور')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (861, 21, N'هجدک')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (862, 21, N'بهرمان')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (863, 21, N'رفسنجان')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (864, 21, N'صفاييه')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (865, 21, N'کشکوييه')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (866, 21, N'مس سرچشمه')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (867, 21, N'رودبار')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (868, 21, N'زهکلوت')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (869, 21, N'گنبکي')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (870, 21, N'محمدآباد')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (871, 21, N'خانوک')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (872, 21, N'ريحان')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (873, 21, N'زرند')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (874, 21, N'يزدان شهر')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (875, 21, N'بلورد')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (876, 21, N'پاريز')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (877, 21, N'خواجو شهر')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (878, 21, N'زيدآباد')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (879, 21, N'سيرجان')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (880, 21, N'نجف شهر')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (881, 21, N'هماشهر')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (882, 21, N'جوزم')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (883, 21, N'خاتون اباد')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (884, 21, N'خورسند')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (885, 21, N'دهج')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (886, 21, N'شهربابک')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (887, 21, N'دوساري')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (888, 21, N'عنبرآباد')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (889, 21, N'مردهک')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (890, 21, N'فارياب')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (891, 21, N'فهرج')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (892, 21, N'قلعه گنج')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (893, 21, N'اختيارآباد')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (894, 21, N'اندوهجرد')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (895, 21, N'باغين')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (896, 21, N'جوپار')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (897, 21, N'چترود')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (898, 21, N'راين')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (899, 21, N'زنگي آباد')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (900, 21, N'شهداد')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (901, 21, N'کاظم آباد')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (902, 21, N'کرمان')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (903, 21, N'گلباف')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (904, 21, N'ماهان')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (905, 21, N'محي آباد')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (906, 21, N'کوهبنان')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (907, 21, N'کيانشهر')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (908, 21, N'کهنوج')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (909, 21, N'منوجان')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (910, 21, N'نودژ')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (911, 21, N'نرماشير')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (912, 21, N'نظام شهر')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (913, 22, N'اسلام آبادغرب')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (914, 22, N'حميل')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (915, 22, N'بانوره')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (916, 22, N'باينگان')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (917, 22, N'پاوه')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (918, 22, N'نودشه')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (919, 22, N'نوسود')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (920, 22, N'ازگله')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (921, 22, N'تازه آباد')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (922, 22, N'جوانرود')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (923, 22, N'ريجاب')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (924, 22, N'کرند')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (925, 22, N'گهواره')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (926, 22, N'روانسر')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (927, 22, N'شاهو')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (928, 22, N'سرپل ذهاب')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (929, 22, N'سطر')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (930, 22, N'سنقر')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (931, 22, N'صحنه')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (932, 22, N'ميان راهان')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (933, 22, N'سومار')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (934, 22, N'قصرشيرين')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (935, 22, N'رباط')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (936, 22, N'کرمانشاه')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (937, 22, N'کوزران')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (938, 22, N'هلشي')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (939, 22, N'کنگاور')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (940, 22, N'گودين')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (941, 22, N'سرمست')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (942, 22, N'گيلانغرب')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (943, 22, N'بيستون')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (944, 22, N'هرسين')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (945, 23, N'باشت')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (946, 23, N'چيتاب')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (947, 23, N'گراب سفلي')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (948, 23, N'مادوان')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (949, 23, N'مارگون')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (950, 23, N'ياسوج')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (951, 23, N'ليکک')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (952, 23, N'چرام')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (953, 23, N'سرفارياب')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (954, 23, N'پاتاوه')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (955, 23, N'سي سخت')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (956, 23, N'دهدشت')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (957, 23, N'ديشموک')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (958, 23, N'سوق')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (959, 23, N'قلعه رييسي')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (960, 23, N'دوگنبدان')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (961, 23, N'لنده')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (962, 24, N'آزادشهر')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (963, 24, N'نگين شهر')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (964, 24, N'نوده خاندوز')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (965, 24, N'آق قلا')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (966, 24, N'انبارآلوم')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (967, 24, N'بندرگز')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (968, 24, N'نوکنده')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (969, 24, N'بندرترکمن')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (970, 24, N'تاتارعليا')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (971, 24, N'خان ببين')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (972, 24, N'دلند')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (973, 24, N'راميان')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (974, 24, N'سنگدوين')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (975, 24, N'علي اباد')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (976, 24, N'فاضل آباد')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (977, 24, N'مزرعه')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (978, 24, N'کردکوي')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (979, 24, N'فراغي')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (980, 24, N'کلاله')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (981, 24, N'گاليکش')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (982, 24, N'جلين')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (983, 24, N'سرخنکلاته')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (984, 24, N'گرگان')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (985, 24, N'سيمين شهر')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (986, 24, N'گميش تپه')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (987, 24, N'اينچه برون')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (988, 24, N'گنبدکاووس')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (989, 24, N'مراوه')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (990, 24, N'مينودشت')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (991, 25, N'آستارا')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (992, 25, N'لوندويل')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (993, 25, N'آستانه اشرفيه')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (994, 25, N'کياشهر')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (995, 25, N'املش')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (996, 25, N'رانکوه')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (997, 25, N'بندرانزلي')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (998, 25, N'خشکبيجار')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (999, 25, N'خمام')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (1000, 25, N'رشت')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (1001, 25, N'سنگر')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (1002, 25, N'کوچصفهان')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (1003, 25, N'لشت نشاء')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (1004, 25, N'لولمان')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (1005, 25, N'پره سر')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (1006, 25, N'رضوانشهر')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (1007, 25, N'بره سر')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (1008, 25, N'توتکابن')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (1009, 25, N'جيرنده')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (1010, 25, N'رستم آباد')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (1011, 25, N'رودبار')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (1012, 25, N'لوشان')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (1013, 25, N'منجيل')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (1014, 25, N'چابکسر')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (1015, 25, N'رحيم آباد')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (1016, 25, N'رودسر')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (1017, 25, N'کلاچاي')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (1018, 25, N'واجارگاه')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (1019, 25, N'ديلمان')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (1020, 25, N'سياهکل')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (1021, 25, N'احمدسرگوراب')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (1022, 25, N'شفت')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (1023, 25, N'صومعه سرا')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (1024, 25, N'گوراب زرميخ')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (1025, 25, N'مرجقل')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (1026, 25, N'اسالم')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (1027, 25, N'چوبر')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (1028, 25, N'حويق')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (1029, 25, N'ليسار')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (1030, 25, N'هشتپر (تالش)')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (1031, 25, N'فومن')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (1032, 25, N'ماسوله')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (1033, 25, N'ماکلوان')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (1034, 25, N'رودبنه')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (1035, 25, N'لاهيجان')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (1036, 25, N'اطاقور')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (1037, 25, N'چاف و چمخاله')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (1038, 25, N'شلمان')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (1039, 25, N'کومله')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (1040, 25, N'لنگرود')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (1041, 25, N'بازار جمعه')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (1042, 25, N'ماسال')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (1043, 26, N'ازنا')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (1044, 26, N'مومن آباد')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (1045, 26, N'اليگودرز')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (1046, 26, N'شول آباد')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (1047, 26, N'اشترينان')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (1048, 26, N'بروجرد')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (1049, 26, N'پلدختر')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (1050, 26, N'معمولان')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (1051, 26, N'بيران شهر')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (1052, 26, N'خرم آباد')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (1053, 26, N'زاغه')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (1054, 26, N'سپيددشت')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (1055, 26, N'نورآباد')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (1056, 26, N'هفت چشمه')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (1057, 26, N'چالانچولان')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (1058, 26, N'دورود')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (1059, 26, N'سراب دوره')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (1060, 26, N'ويسيان')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (1061, 26, N'چقابل')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (1062, 26, N'الشتر')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (1063, 26, N'فيروزآباد')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (1064, 26, N'درب گنبد')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (1065, 26, N'کوهدشت')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (1066, 26, N'کوهناني')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (1067, 26, N'گراب')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (1068, 27, N'آمل')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (1069, 27, N'امامزاده عبدالله')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (1070, 27, N'دابودشت')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (1071, 27, N'رينه')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (1072, 27, N'گزنک')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (1073, 27, N'اميرکلا')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (1074, 27, N'بابل')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (1075, 27, N'خوش رودپي')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (1076, 27, N'زرگرمحله')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (1077, 27, N'گتاب')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (1078, 27, N'گلوگاه')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (1079, 27, N'مرزيکلا')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (1080, 27, N'بابلسر')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (1081, 27, N'بهنمير')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (1082, 27, N'هادي شهر')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (1083, 27, N'بهشهر')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (1084, 27, N'خليل شهر')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (1085, 27, N'رستمکلا')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (1086, 27, N'تنکابن')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (1087, 27, N'خرم آباد')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (1088, 27, N'شيرود')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (1089, 27, N'نشتارود')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (1090, 27, N'جويبار')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (1091, 27, N'کوهي خيل')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (1092, 27, N'چالوس')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (1093, 27, N'مرزن آباد')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (1094, 27, N'هچيرود')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (1095, 27, N'رامسر')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (1096, 27, N'کتالم وسادات شهر')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (1097, 27, N'پايين هولار')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (1098, 27, N'ساري')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (1099, 27, N'فريم')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (1100, 27, N'کياسر')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (1101, 27, N'آلاشت')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (1102, 27, N'پل سفيد')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (1103, 27, N'زيرآب')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (1104, 27, N'شيرگاه')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (1105, 27, N'کياکلا')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (1106, 27, N'سلمان شهر')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (1107, 27, N'عباس اباد')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (1108, 27, N'کلارآباد')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (1109, 27, N'فريدونکنار')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (1110, 27, N'ارطه')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (1111, 27, N'قايم شهر')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (1112, 27, N'کلاردشت')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (1113, 27, N'گلوگاه')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (1114, 27, N'سرخرود')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (1115, 27, N'محمودآباد')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (1116, 27, N'سورک')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (1117, 27, N'نکا')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (1118, 27, N'ايزدشهر')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (1119, 27, N'بلده')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (1120, 27, N'چمستان')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (1121, 27, N'رويان')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (1122, 27, N'نور')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (1123, 27, N'پول')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (1124, 27, N'کجور')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (1125, 27, N'نوشهر')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (1126, 28, N'آشتيان')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (1127, 28, N'اراک')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (1128, 28, N'داودآباد')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (1129, 28, N'ساروق')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (1130, 28, N'کارچان')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (1131, 28, N'تفرش')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (1132, 28, N'خمين')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (1133, 28, N'قورچي باشي')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (1134, 28, N'جاورسيان')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (1135, 28, N'خنداب')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (1136, 28, N'دليجان')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (1137, 28, N'نراق')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (1138, 28, N'پرندک')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (1139, 28, N'خشکرود')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (1140, 28, N'رازقان')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (1141, 28, N'زاويه')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (1142, 28, N'مامونيه')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (1143, 28, N'آوه')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (1144, 28, N'ساوه')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (1145, 28, N'غرق آباد')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (1146, 28, N'نوبران')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (1147, 28, N'آستانه')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (1148, 28, N'توره')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (1149, 28, N'شازند')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (1150, 28, N'شهباز')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (1151, 28, N'مهاجران')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (1152, 28, N'هندودر')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (1153, 28, N'خنجين')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (1154, 28, N'فرمهين')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (1155, 28, N'کميجان')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (1156, 28, N'ميلاجرد')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (1157, 28, N'محلات')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (1158, 28, N'نيمور')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (1159, 29, N'ابوموسي')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (1160, 29, N'بستک')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (1161, 29, N'جناح')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (1162, 29, N'سردشت')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (1163, 29, N'گوهران')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (1164, 29, N'بندرعباس')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (1165, 29, N'تازيان پايين')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (1166, 29, N'تخت')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (1167, 29, N'فين')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (1168, 29, N'قلعه قاضي')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (1169, 29, N'بندرلنگه')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (1170, 29, N'چارک')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (1171, 29, N'کنگ')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (1172, 29, N'کيش')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (1173, 29, N'لمزان')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (1174, 29, N'پارسيان')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (1175, 29, N'دشتي')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (1176, 29, N'کوشکنار')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (1177, 29, N'بندرجاسک')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (1178, 29, N'حاجي اباد')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (1179, 29, N'سرگز')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (1180, 29, N'فارغان')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (1181, 29, N'خمير')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (1182, 29, N'رويدر')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (1183, 29, N'بيکاء')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (1184, 29, N'دهبارز')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (1185, 29, N'زيارتعلي')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (1186, 29, N'سيريک')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (1187, 29, N'کوهستک')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (1188, 29, N'گروک')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (1189, 29, N'درگهان')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (1190, 29, N'سوزا')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (1191, 29, N'قشم')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (1192, 29, N'هرمز')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (1193, 29, N'تيرور')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (1194, 29, N'سندرک')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (1195, 29, N'ميناب')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (1196, 29, N'هشتبندي')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (1197, 30, N'آجين')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (1198, 30, N'اسدآباد')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (1199, 30, N'بهار')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (1200, 30, N'صالح آباد')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (1201, 30, N'لالجين')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (1202, 30, N'مهاجران')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (1203, 30, N'تويسرکان')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (1204, 30, N'سرکان')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (1205, 30, N'فرسفج')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (1206, 30, N'دمق')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (1207, 30, N'رزن')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (1208, 30, N'قروه درجزين')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (1209, 30, N'فامنين')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (1210, 30, N'شيرين سو')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (1211, 30, N'کبودرآهنگ')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (1212, 30, N'گل تپه')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (1213, 30, N'ازندريان')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (1214, 30, N'جوکار')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (1215, 30, N'زنگنه')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (1216, 30, N'سامن')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (1217, 30, N'ملاير')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (1218, 30, N'برزول')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (1219, 30, N'فيروزان')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (1220, 30, N'گيان')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (1221, 30, N'نهاوند')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (1222, 30, N'جورقان')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (1223, 30, N'قهاوند')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (1224, 30, N'مريانج')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (1225, 30, N'همدان')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (1226, 31, N'ابرکوه')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (1227, 31, N'مهردشت')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (1228, 31, N'احمدآباد')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (1229, 31, N'اردکان')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (1230, 31, N'عقدا')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (1231, 31, N'اشکذر')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (1232, 31, N'خضرآباد')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (1233, 31, N'بافق')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (1234, 31, N'بهاباد')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (1235, 31, N'تفت')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (1236, 31, N'نير')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (1237, 31, N'مروست')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (1238, 31, N'هرات')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (1239, 31, N'مهريز')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (1240, 31, N'بفروييه')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (1241, 31, N'ميبد')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (1242, 31, N'ندوشن')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (1243, 31, N'حميديا')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (1244, 31, N'زارچ')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (1245, 31, N'شاهديه')
GO
INSERT [dbo].[City] ([CityId], [ProvinceId], [CityName]) VALUES (1246, 31, N'يزد')
GO
SET IDENTITY_INSERT [dbo].[City] OFF
GO
