﻿
/*!
* Data Aquarium Framework - Resources
* Copyright 2008-2018 Code On Time LLC; Licensed MIT; http://codeontime.com/license
*/
(function () {
    Type.registerNamespace('Web');

    var _dvr = Web.DataViewResources = {};

    _dvr.Common = {
        WaitHtml: '<div class="Wait"></div>'
    };

    _dvr.Pager = {
        ItemsPerPage: '每页显示：',
        PageSizes: [10, 15, 20, 25],
        ShowingItems: '显示项目\u003cb\u003e{0}\u003c/b\u003e \u003cb\u003e{1}\u003c/b\u003e - \u003cb\u003e{2}\u003c/b\u003e',
        SelectionInfo: ' (<b>{0}</b> selected)',
        Refresh: '刷新',
        Next: '下一页 »',
        Previous: '« 上一页',
        Page: '页',
        PageButtonCount: 10
    };

    _dvr.ActionBar = {
        View: '查看'
    };

    _dvr.ModalPopup = {
        Close: '关闭',
        MaxWidth: 800,
        MaxHeight: 600,
        OkButton: 'OK',
        CancelButton: '取消',
        SaveButton: '保存',
        SaveAndNewButton: '保存并新建'
    };

    _dvr.Menu = {
        SiteActions: '网站操作',
        SeeAlso: '也见',
        Summary: '摘要',
        Tasks: '任务',
        About: '关于'
    };

    _dvr.HeaderFilter = {
        GenericSortAscending: '最小的在最前',
        GenericSortDescending: '上最大的热门',
        StringSortAscending: '递增',
        StringSortDescending: '递减',
        DateSortAscending: '最早在最前面',
        DateSortDescending: '最新热门',
        EmptyValue: '（空）',
        BlankValue: '（空白）',
        Loading: '载入中... ...',
        ClearFilter: '从清除筛选{0}',
        SortBy: '排序{0}',
        MaxSampleTextLen: 80,
        CustomFilterOption: '过滤器...'
    };

    _dvr.InfoBar = {
        FilterApplied: '过滤器得到了应用。',
        ValueIs: ' <span class="Highlight">{0}</span> ',
        Or: ' 或是 ',
        And: ' ，并 ',
        EqualTo: '等于 ',
        LessThan: '小于 ',
        LessThanOrEqual: '小于或等于 ',
        GreaterThan: '大于 ',
        GreaterThanOrEqual: '大于或等于 ',
        Like: '就像 ',
        StartsWith: '开始 ',
        Empty: '空',
        QuickFind: ' 任何字段包含 '
    };

    _dvr.Lookup = {
        SelectToolTip: '选择{0}',
        ClearToolTip: '清除{0}',
        NewToolTip: '新{0}',
        SelectLink: '（选择）',
        ShowActionBar: true,
        DetailsToolTip: '查看详细{0}',
        ShowDetailsInPopup: true,
        GenericNewToolTip: '新建',
        AddItem: '新增项目'
    };

    _dvr.Validator = {
        Required: '需要',
        RequiredField: '此字段是必需的。',
        EnforceRequiredFieldsWithDefaultValue: false,
        NumberIsExpected: '一个数字的预期。',
        BooleanIsExpected: '一个逻辑值的预期。',
        DateIsExpected: '阿日预期。',
        Optional: '可选的'
    };

    var _mn = Sys.CultureInfo.CurrentCulture.dateTimeFormat.MonthNames;

    _dvr.Data = {
        ConnectionLost: '网络连接已丢失。再试一次吗？',
        AnyValue: '（任何）',
        NullValue: '<span class="NA">N/A</span>',
        NullValueInForms: 'N/A',
        BooleanDefaultStyle: 'DropDownList',
        BooleanOptionalDefaultItems: [[null, 'N/A'], [false, '无'], [true, '是的']],
        BooleanDefaultItems: [[false, '无'], [true, '是的']],
        MaxReadOnlyStringLen: 600,
        NoRecords: '没有找到记录。',
        BlobHandler: 'Blob.ashx',
        BlobDownloadLink: '下载',
        BlobDownloadLinkReadOnly: '<span style="color:gray;">下载</span>',
        BlobDownloadHint: '点击这里下载原始文件。',
        BlobDownloadOriginalHint: '点击图片下载原始文件。',
        BatchUpdate: '更新',
        NoteEditLabel: '修改',
        NoteDeleteLabel: '删除',
        NoteDeleteConfirm: '删除？',
        UseLEV: '粘贴\"{0}\"',
        DiscardChanges: '放弃更改？',
        KeepOriginalSel: 'keep original selection',
        DeleteOriginalSel: 'delete original selection',
        Import: {
            UploadInstruction: '请选择要上传的文件。该文件必须在\u003cb\u003eCSV\u003c/b\u003e，\u003cb\u003eXLS\u003c/b\u003e，或\u003cb\u003eXLSX\u003c/b\u003e格式。',
            DownloadTemplate: '下载导入文件模板。',
            Uploading: '您的文件正在上传到服务器。请稍候...',
            MappingInstruction: '文件中有\u003cb\u003e{0}\u003c/b\u003e \u003cb\u003e{1}\u003c/b\u003e记录（S）准备processed.\u003cbr/\u003ePlease映射字段从数据文件导入到应用领域和点击\u003ci\u003eImport\u003c/i\u003e开始处理。',
            StartButton: '进口',
            AutoDetect: '（自动检测）',
            Processing: '导入文件处理已经开始。导入的数据记录后，可以成功处理。您可能需要重新整理有关意见/页查看导入的记录。',
            Email: '导入日志发送到以下电子邮件地址（可选）',
            EmailNotSpecified: '在导入日志收件人未指定。继续呢？'
        },
        Filters: {
            Labels: {
                And: '，并',
                Or: '或是',
                Equals: '等于',
                Clear: '清除',
                SelectAll: '（全选）',
                Includes: '包括',
                FilterToolTip: '更改'
            },
            Number: {
                Text: '号码过滤器',
                Kind: '号码',
                List: [
                    { Function: '=', Text: '等于', Prompt: true },
                    { Function: '<>', Text: '不等于', Prompt: true },
                    { Function: '<', Text: '小于', Prompt: true },
                    { Function: '>', Text: '大于', Prompt: true },
                    { Function: '<=', Text: '小于或等于', Prompt: true },
                    { Function: '>=', Text: '大于或等于', Prompt: true },
                    { Function: '$between', Text: '之间', Prompt: true },
                    { Function: '$in', Text: '包括', Prompt: true, Hidden: true },
                    { Function: '$notin', Text: '不包括', Prompt: true, Hidden: true },
                    { Function: '$isnotempty', Text: '没有空' },
                    { Function: '$isempty', Text: '空' }
                ]
            },
            Text: {
                Text: '文本过滤器',
                Kind: '文本',
                List: [
                    { Function: '=', Text: '等于', Prompt: true },
                    { Function: '<>', Text: '不等于', Prompt: true },
                    { Function: '$beginswith', Text: '开始', Prompt: true },
                    { Function: '$doesnotbeginwith', Text: '不以', Prompt: true },
                    { Function: '$contains', Text: '包含', Prompt: true },
                    { Function: '$doesnotcontain', Text: '不包含', Prompt: true },
                    { Function: '$endswith', Text: '结尾为', Prompt: true },
                    { Function: '$doesnotendwith', Text: '并没有结​​束', Prompt: true },
                    { Function: '$in', Text: '包括', Prompt: true, Hidden: true },
                    { Function: '$notin', Text: '不包括', Prompt: true, Hidden: true },
                    { Function: '$isnotempty', Text: '没有空' },
                    { Function: '$isempty', Text: '空' }
                ]
            },
            Boolean: {
                Text: '逻辑过滤器',
                Kind: '合乎逻辑的',
                List: [
                    { Function: '$true', Text: '是的' },
                    { Function: '$false', Text: '无' },
                    { Function: '$isnotempty', Text: '没有空' },
                    { Function: '$isempty', Text: '空' }
                ]
            },
            Date: {
                Text: '日期过滤器',
                Kind: '日期',
                List: [
                    { Function: '=', Text: '等于', Prompt: true },
                    { Function: '<>', Text: '不等于', Prompt: true },
                    { Function: '<', Text: '前', Prompt: true },
                    { Function: '>', Text: '后', Prompt: true },
                    { Function: '<=', Text: '或之前', Prompt: true },
                    { Function: '>=', Text: '当日或之后', Prompt: true },
                    { Function: '$between', Text: '之间', Prompt: true },
                    { Function: '$in', Text: '包括', Prompt: true, Hidden: true },
                    { Function: '$notin', Text: '不包括', Prompt: true, Hidden: true },
                    { Function: '$isnotempty', Text: '没有空' },
                    { Function: '$isempty', Text: '空' },
                    null,
                    { Function: '$tomorrow', Text: '明天' },
                    { Function: '$today', Text: '今天' },
                    { Function: '$yesterday', Text: '昨天' },
                    null,
                    { Function: '$nextweek', Text: '下一个周' },
                    { Function: '$thisweek', Text: '这周' },
                    { Function: '$lastweek', Text: '最后周' },
                    null,
                    { Function: '$nextmonth', Text: '下一页月' },
                    { Function: '$thismonth', Text: '本月' },
                    { Function: '$lastmonth', Text: '最后月' },
                    null,
                    { Function: '$nextquarter', Text: '下季度' },
                    { Function: '$thisquarter', Text: '本季' },
                    { Function: '$lastquarter', Text: '上季度' },
                    null,
                    { Function: '$nextyear', Text: '下年' },
                    { Function: '$thisyear', Text: '今年' },
                    { Function: '$yeartodate', Text: '本年度截止到' },
                    { Function: '$lastyear', Text: '去年年' },
                    null,
                    { Function: '$past', Text: '过去' },
                    { Function: '$future', Text: '未来' },
                    null,
                    {
                        Text: '在期间所有日期',
                        List: [
                            { Function: '$quarter1', Text: '第一季度' },
                            { Function: '$quarter2', Text: '第2季度' },
                            { Function: '$quarter3', Text: '第三季' },
                            { Function: '$quarter4', Text: '第四季度' },
                            null,
                            { Function: '$month1', Text: _mn[0] },
                            { Function: '$month2', Text: _mn[1] },
                            { Function: '$month3', Text: _mn[2] },
                            { Function: '$month4', Text: _mn[3] },
                            { Function: '$month5', Text: _mn[4] },
                            { Function: '$month6', Text: _mn[5] },
                            { Function: '$month7', Text: _mn[6] },
                            { Function: '$month8', Text: _mn[7] },
                            { Function: '$month9', Text: _mn[8] },
                            { Function: '$month10', Text: _mn[9] },
                            { Function: '$month11', Text: _mn[10] },
                            { Function: '$month12', Text: _mn[11] }
                        ]
                    }
                ]
            }
        }
    };


    _dvr.Form = {
        ShowActionBar: true,
        ShowCalendarButton: true,
        RequiredFieldMarker: '<span class="Required">*</span>',
        RequiredFiledMarkerFootnote: '* - 表示必填字段',
        SingleButtonRowFieldLimit: 7,
        GeneralTabText: '一般',
        Minimize: '关闭',
        Maximize: '展开'
    };

    _dvr.Grid = {
        InPlaceEditContextMenuEnabled: true,
        QuickFindText: '快速查找',
        QuickFindToolTip: '键入要搜索的记录，然后按Enter',
        ShowAdvancedSearch: '显示高级搜索',
        VisibleSearchBarFields: 3,
        DeleteSearchBarField: '（删除）',
        //AddSearchBarField: '更多搜索字段',
        HideAdvancedSearch: '隐藏高级搜索',
        PerformAdvancedSearch: '搜索',
        ResetAdvancedSearch: '复位',
        NewRowLink: '点击创建一个新的纪录。',
        RootNodeText: '顶级',
        FlatTreeToggle: '切换到层次结构',
        HierarchyTreeModeToggle: '切换到平面列表',
        AddConditionText: '添加搜索条件',
        AddCondition: '添加条件',
        RemoveCondition: '删除条件',
        ActionColumnHeaderText: '动作',
        Aggregates: {
            None: { FmtStr: '', ToolTip: '' },
            Sum: { FmtStr: '萨姆：{0}', ToolTip: '总和{0}' },
            Count: { FmtStr: '统计：{0}', ToolTip: '计数{0}' },
            Avg: { FmtStr: '平均：{0}', ToolTip: '平均{0}' },
            Max: { FmtStr: '马克斯：{0}', ToolTip: '最大{0}' },
            Min: { FmtStr: '闵：{0}', ToolTip: '最低{0}' }
        },
        Freeze: '冻结',
        Unfreeze: '解冻'
    };

    _dvr.Views = {
        DefaultDescriptions: {
            '$DefaultGridViewDescription': '这是一个{0}的清单。',
            '$DefaultEditViewDescription': '请仔细阅读以下信息{0}。单击编辑来改变这个记录，点击删除，删除记录，或单击取消/关闭返回回来。',
            '$DefaultCreateViewDescription': '请填写此表格，然后点击OK按钮创建一个新的{0}纪录。单击取消返回到前一个画面。'
        },
        DefaultCategoryDescriptions: {
            '$DefaultEditDescription': '这些是{0}记录的字段，可以进行编辑。',
            '$DefaultNewDescription': '完成表格。请务必输入所有必需的字段。',
            '$DefaultReferenceDescription': '约{0}更多细节提供了参考信息部分。'
        }
    };

    _dvr.Actions = {
        Scopes: {
            'Grid': {
                'Select': {
                    HeaderText: '选择'
                },
                'Edit': {
                    HeaderText: '编辑'
                },
                'Delete': {
                    HeaderText: '删除',
                    Confirmation: '删除？',
                    Notify: '{$ selected}已删除'
                },
                'Duplicate': {
                    HeaderText: '重复'
                },
                'New': {
                    HeaderText: '新'
                },
                'BatchEdit': {
                    HeaderText: '批量修改'
                    //                    ,CommandArgument: {
                    //                        'editForm1': {
                    //                            HeaderText: '批量修改（表）'
                    //                        }
                    //                    }
                },
                'Open': {
                    HeaderText: '打开'
                }
            },
            'Form': {
                'Edit': {
                    HeaderText: '编辑'
                },
                'Delete': {
                    HeaderText: '删除',
                    Confirmation: '删除？',
                    Notify: '{$ selected}已删除'
                },
                'Cancel': {
                    HeaderText: '关闭',
                    WhenLastCommandName: {
                        'Duplicate': {
                            HeaderText: '取消'
                        },
                        'Edit': {
                            HeaderText: '取消'
                        },
                        'New': {
                            HeaderText: '取消'
                        }

                    }
                },
                'Update': {
                    HeaderText: 'OK',
                    Notify: '已保存 -  {0}',
                    CommandArgument: {
                        'Save': {
                            HeaderText: '保存',
                            Notify: '已保存 -  {0}'
                        }
                    },
                    WhenLastCommandName: {
                        'BatchEdit': {
                            HeaderText: '更新的选择',
                            Confirmation: '更新？',
                            Notify: '已保存 -  {0}'
                        }
                    }
                },
                'Insert': {
                    HeaderText: 'OK',
                    Notify: '已保存 -  {0}',
                    CommandArgument: {
                        'Save': {
                            HeaderText: '保存',
                            Notify: '已保存 -  {0}'
                        },
                        'SaveAndNew': {
                            HeaderText: '保存并新建',
                            Notify: '已保存 -  {0}'
                        }
                    }
                },
                'Confirm': {
                    HeaderText: 'OK'
                },
                'Navigate': {
                    Controller: {
                        'SiteContent': {
                            HeaderText: '添加系统身份'
                        }
                    }
                }
            },
            'ActionBar': {
                _Self: {
                    'Actions': {
                        HeaderText: '动作'
                    },
                    'Report': {
                        HeaderText: '报告'
                    },
                    'Record': {
                        HeaderText: '记录'
                    }
                },
                'New': {
                    HeaderText: '新{0}',
                    Description: '创建一个新的{0}纪录。',
                    HeaderText2: '新',
                    VarMaxLen: 15
                },
                'Edit': {
                    HeaderText: '编辑'
                },
                'Delete': {
                    HeaderText: '删除',
                    Confirmation: '删除？',
                    Notify: '{$ selected}已删除'
                },
                'ExportCsv': {
                    HeaderText: '下载',
                    Description: '以CSV格式下载项目。'
                },
                'ExportRowset': {
                    HeaderText: '导出到电子表格',
                    Description: '分析与spreadsheet\u003cbr/\u003eapplication项目。'
                },
                'ExportRss': {
                    HeaderText: '查看RSS提要',
                    Description: '用RSS阅读器辛迪加项目。'
                },
                'Import': {
                    HeaderText: '从文件导入',
                    Description: '上传CSV，XLS，XLSX file\u003cbr/\u003eto或进口的记录。'
                },
                'Update': {
                    HeaderText: '保存',
                    Description: '保存到数据库的变化。',
                    Notify: '已保存 -  {0}'
                },
                'Insert': {
                    HeaderText: '保存',
                    Description: '保存新的记录到数据库中。',
                    Notify: '已保存 -  {0}'
                },
                'Cancel': {
                    HeaderText: '取消',
                    WhenLastCommandName: {
                        'Edit': {
                            HeaderText: '取消',
                            Description: '取消所有记录的变化。'
                        },
                        'New': {
                            HeaderText: '取消',
                            Description: '取消新纪录。'
                        }
                    }
                },
                'Report': {
                    HeaderText: '报告',
                    Description: '渲染PDF格式的报告'
                },
                'ReportAsPdf': {
                    HeaderText: 'PDF文档',
                    Description: '查看项目为Adobe PDF document.\u003cbr/\u003eRequires兼容读卡器。'
                },
                'ReportAsImage': {
                    HeaderText: '多页图像',
                    Description: '查看多页TIFF图像作为一个项目。'
                },
                'ReportAsExcel': {
                    HeaderText: '电子表格',
                    Description: '查看在formatted\u003cbr/\u003eMicrosoft Excel电子表格的项目。'
                },
                'ReportAsWord': {
                    HeaderText: '微软Word',
                    Description: '在fo​​rmatted\u003cbr/\u003eMicrosoft查看Word文档项目。'
                },
                'DataSheet': {
                    HeaderText: '显示数据表',
                    Description: '显示项使用数据sheet\u003cbr/\u003eformat。'
                },
                'Grid': {
                    HeaderText: '显示在标准视图',
                    Description: '显示在standard\u003cbr/\u003elist格式项目。'
                },
                'Tree': {
                    HeaderText: '显示层次结构',
                    Description: '显示层次关系。'
                },
                'Search': {
                    HeaderText: '搜索',
                    Description: '搜索 {0}'
                },
                'Upload': {
                    HeaderText: 'Upload',
                    Description: 'Upload multiple files.'
                }
            },
            'Row': {
                'Update': {
                    HeaderText: '保存',
                    Notify: '已保存 -  {0}',
                    WhenLastCommandName: {
                        'BatchEdit': {
                            HeaderText: '更新的选择',
                            Confirmation: '更新？'
                        }
                    }
                },
                'Insert': {
                    HeaderText: '插入',
                    Notify: '已保存 -  {0}'
                },
                'Cancel': {
                    HeaderText: '取消'
                }
            },
            'ActionColumn': {
                'Edit': {
                    HeaderText: '编辑'
                },
                'Delete': {
                    HeaderText: '删除',
                    Confirmation: '删除？',
                    Notify: '{$ selected}已删除'
                }
            }
        }
    };

    _dvr.Editor = {
        Undo: '复原',
        Redo: '重做',
        Bold: '胆大',
        Italic: '斜体',
        Underline: '强调',
        Strikethrough: '透',
        Subscript: '小脚本',
        Superscript: '超级脚本',
        JustifyLeft: '左对齐',
        JustifyCenter: '对齐中心',
        JustifyRight: '右对齐',
        JustifyFull: '两边对齐',
        InsertOrderedList: '插入有序列表',
        InsertUnorderedList: '插入无序列表',
        CreateLink: '创建链接',
        UnLink: '断开',
        RemoveFormat: '删除格式',
        SelectAll: '全选',
        UnSelect: '取消选择',
        Delete: '删除',
        Cut: '切',
        Copy: '复制',
        Paste: '贴',
        BackColor: '背景颜色',
        ForeColor: '前景色',
        FontName: '字体名称',
        FontSize: '字体大小',
        Indent: '缩进',
        Outdent: '凸出',
        InsertHorizontalRule: '插入水平线',
        HorizontalSeparator: '分离器',
        Format: '格式',
        FormatBlock: {
            p: '段',
            blockquote: '行情',
            h1: '标题1',
            h2: '标题2',
            h3: '标题3',
            h4: '标题4',
            h5: '标题5',
            h6: '标题6'
        },
        Rtf: {
            editor: '全屏'
        }
    };

    _dvr.Mobile = {
        UpOneLevel: '上一层',
        Back: '回归',
        BatchEdited: '{0} 更新',
        Sort: '分类',
        Sorted: '排序',
        SortedDefault: '默认排序顺序。',
        SortByField: '选择一个字段来改变\u003cb\u003e{0}\u003c/b\u003e的排序顺序。',
        SortByOptions: '选择的排序顺序\u003cb\u003e{0}\u003c/b\u003e由现场\u003cb\u003e{1}\u003c/b\u003e在以下选项列表。',
        DefaultOption: '标准',
        Auto: '汽车',
        Filter: '过滤器',
        List: '名单',
        Cards: '牌',
        Grid: '格',
        Map: '地图',
        Calendar: '日历',
        ZoomIn: '放大',
        ZoomOut: '缩小',
        Directions: '路线',
        AlternativeView: '选择数据的另一种观点。',
        PresentationStyle: '选择一个数据表现风格。',
        LookupViewAction: '查看',
        LookupSelectAction: '选择',
        LookupClearAction: '清除',
        LookupNewAction: '新',
        LookupInstruction: '请选择\u003cb\u003e{0}\u003c/b\u003e在列表中。',
        LookupOriginalSelection: '原来选择的是\u003cb\u003e“{0}”\u003c/b\u003e。',
        EmptyContext: '操作不可用。',
        Favorites: '我的最爱',
        History: '历史',
        FilterSiteMap: '过滤网站地图...',
        ResumeLookup: '简历查询',
        ResumeEntering: '进入恢复',
        ResumeEditing: '编辑简历',
        ResumeBrowsing: '继续浏览',
        ResumeViewing: '查看简历',
        Menu: '菜单',
        Home: '家',
        Settings: '设置',
        Sidebar: '边栏',
        Landscape: '景观',
        Portrait: '肖像',
        Never: '从来没有',
        Always: '总是',
        ShowSystemButtons: '显示系统按钮',
        OnHover: '悬停',
        ButtonShapes: '按钮形状',
        PromoteActions: '促进行动',
        ConfirmReload: '重装？',
        ClearText: '清除文​​本',
        SeeAll: '查看全部',
        More: '更多',
        TouchUINotSupported: '触摸用户界面是不是在这个浏览器的支持。',
        ShowingItemsInfo: '显示{0}项。',
        FilterByField: '选择一个字段应用一个过滤器，\u003cb\u003e{0}\u003c/b\u003e。',
        Apply: '应用',
        FilterByOptions: '由现场选择在下面的列表中，按\u003cb\u003e{2}\u003c/b\u003e过滤\u003cb\u003e{0}\u003c/b\u003e一个或多个选项\u003cb\u003e{1}\u003c/b\u003e。',
        Suggestions: '建议',
        UnSelect: '取消选择',
        AdvancedSearch: '高级搜索',
        QuickFindScope: '在搜索...',
        QuickFindDescription: 'Search in {0}',
        AddMatchingGroup: '添加匹配组',
        MatchAll: '符合所有条件',
        MatchAny: '匹配任何条件',
        DoNotMatchAll: '不符合条件的所有',
        DoNotMatchAny: '不符合任何条件',
        MatchAllPastTense: '匹配所有条件',
        MatchAnyPastTense: '匹配任何条件',
        DoNotMatchAllPastTense: '没有匹配的所有条件',
        DoNotMatchAnyPastTense: '没有匹配任何条件',
        In: '在',
        Recent: '最近',
        Matched: '匹配',
        DidNotMatch: '不匹配',
        ClearFilter: '清除过滤器',
        ResetSearchConfirm: '重置搜索条件？',
        FilterCleared: '清除所有过滤器。',
        AdvancedSearchInstruction: '输入必须匹配的条件，按搜索按钮。',
        Refreshed: '装修一新',
        Group: '组',
        Grouped: '分组',
        UnGrouped: '分组已被删除',
        GroupedBy: '通过分组',
        GroupByField: '选择一个字段组\u003cb\u003e{0}\u003c/b\u003e。',
        Show: '显示',
        Hide: '隐藏',
        None: '无',
        Next: '下一个',
        Prev: '上一页',
        FitToWidth: '适合宽度',
        MultiSelection: '多重选择',
        InlineEditing: '内联编辑',
        ItemsSelectedOne: '{0}项选择',
        ItemsSelectedMany: '选择{0}项目',
        TypeToSearch: '键入搜索',
        NoMatches: '没有球赛。',
        ShowingItemsRange: '显示{0} {1}的物品',
        Finish: '完',
        ShowOptions: '显示选项',
        ConfirmContinue: '继续？',
        AddAccount: '新增帐户',
        Fullscreen: '全屏',
        ExitFullscreen: '退出全屏',
        Apps: '应用',
        Forget: '忘记',
        ManageAccounts: '管理帐户',
        SignedOut: '退出',
        Submit: '提交',
        Error: '错误',
        Line: '线',
        Download: '下载',
        Orientation: '方向',
        Device: '设备',
        ShowMore: '展示更多',
        ShowLess: '显示较少',
        WithSpecifiedFilters: '使用指定的过滤器',
        WithSelectedValues5: '选择值（前5名）',
        WithSelectedValues10: '选择值（十大）',
        ReadOnly: '{0}是只读的。',
        InlineCommands: {
            List: {
                Select: '选择物品',
                Edit: '编辑项目',
                New: '新物品',
                Duplicate: '重复项目',
            },
            Grid: {
                Select: '选择行',
                Edit: '编辑行',
                New: '新行',
                Duplicate: '重复行',
            }
        },
        DisplayDensity: {
            Label: '显示密度',
            List: {
                Tiny: '小',
                Condensed: '简明',
                Compact: '紧凑',
                Comfortable: '自在'
            }
        },
        Files: {
            KB: 'KB',
            MB: 'KB',
            Bytes: '字节',
            Drop: '在这里放一个文件',
            DropMany: '此文件拖放',
            Tap: '点击选择文件',
            TapMany: '点击选择文件',
            Click: '单击选择文件',
            ClickMany: '单击选择文件',
            Clear: '明确',
            ClearConfirm: '清楚了吗？',
            Sign: '在这里注册',
            Cleared: 'Value will be cleared on save'
        },
        Import: {
            SelectFile: '选择在CSV，XLS或XLSX格式的数据文件。',
            NotSupported: '不支持的“{0}”数据格式。',
            NotMatched: '（不匹配）',
            FileStats: '有文件中的记录\u003cb\u003e{0}\u003c/b\u003e \u003cb\u003e{1}\u003c/b\u003e准备进行处理。请匹配字段名称。',
            Importing: '输入',
            Into: '成',
            StartImport: '开始导入',
            InsertingRecords: '插入记录',
            TestingRecords: '测试记录',
            ResolvingReferences: '解析引用',
            Complete: '完成',
            Expected: '预计完成',
            Remaining: '预计完成',
            Done: '完成进口',
            Duplicates: '重复'
        },
        Themes: {
            Label: '主题',
            Accent: '口音',
            List: {
                None: '没有',
                Light: '光',
                Dark: '暗',
                Aquarium: '水族馆',
                Azure: '天蓝',
                Belltown: '贝尔镇',
                Berry: '浆果',
                Bittersweet: '苦乐参半',
                Cay: '岩礁',
                Citrus: '柑橘',
                Classic: '经典',
                Construct: '建设',
                Convention: '惯例',
                DarkKnight: '黑暗骑士',
                Felt: '毡',
                Graham: '格雷厄姆',
                Granite: '花岗岩',
                Grapello: 'Grapello',
                Gravity: '重力',
                Lacquer: '漆',
                Laminate: '层压板',
                Lichen: '青苔',
                Mission: '使命',
                Modern: '现代',
                ModernRose: '现代的玫瑰',
                Municipal: '市',
                Petal: '花瓣',
                Pinnate: '羽状复叶',
                Plastic: '塑料',
                Ricasso: 'Ricasso',
                Simple: '简单',
                Social: '社会的',
                Summer: '夏天',
                Vantage: '华帝',
                Verdant: '葱茏',
                Viewpoint: '观点',
                WhiteSmoke: '白色烟雾',
                Yoshi: '耀西'
            }
        },
        Transitions: {
            Label: '过渡',
            List: {
                none: '无',
                slide: '滑',
                fade: '褪色',
                pop: '流行的',
                flip: '翻动',
                turn: '转',
                flow: '流',
                slideup: '向上滑动',
                slidedown: '向下滑动'
            }
        },
        LabelsInList: {
            Label: '标签在列表',
            List: {
                DisplayedAbove: '上面显示的',
                DisplayedBelow: '显示在下方'
            }
        },
        InitialListMode: {
            Label: '初始列表模式',
            List: {
                SeeAll: '查看全部',
                Summary: '摘要'
            }
        },
        Dates: {
            SmartDates: '智能日期',
            Yesterday: '昨天',
            Last: '持续',
            Today: '今天',
            OneHour: '一小时前',
            MinAgo: '{0}分钟前',
            AMinAgo: '一分钟前',
            InHour: '一个小时内',
            InMin: '在{0}分钟',
            InAMin: '在一分钟内',
            Now: '现在',
            JustNow: '现在',
            Tomorrow: '明天',
            Next: '下一个'
        },
        Sync: {
            Uploading: 'Uploading {0}...'
        },
        Develop: {
            Tools: '开发者工具',
            Explorer: '项目浏览器',
            FormLayout: '表格布局',
            FormLayoutInstr: '选择要包含在布局中的屏幕尺寸。'
        }
    };

    _dvr.Device = {
        Exit: '出口',
        DeviceLoginPrompt: '请登录授权访问此设备上。'
    };

    _dvr.Presenters = {
        Charts: {
            Text: '排行榜',
            DataWarning: '项的过程的最大数目为{0：D}。点击此处筛选结果。',
            ShowData: '显示数据',
            ShowChart: '显示图表',
            Sizes: {
                Label: '尺寸',
                Small: '小',
                Medium: '中',
                Large: '大'
            },
            ChartLabels: {
                By: '由',
                Top: '顶',
                Other: '其他',
                Blank: '空白',
                GrandTotals: '总计',
                CountOf: '伯爵',
                SumOf: '总和',
                AvgOf: '平均',
                MinOf: '最小',
                MaxOf: '最大',
                Quarter: '季',
                Week: '周'
            }
        },
        Calendar: {
            Text: '日历',
            Today: '今天',
            Noon: '中午',
            Year: '年',
            Month: '月',
            Week: '周',
            Day: '日',
            Agenda: '议程',
            Sync: '同步',
            Less: '减'
        }
    };

    // membership resources

    var _mr = Web.MembershipResources = {};

    _mr.Bar = {
        LoginLink: '登录',
        LoginText: ' 本网站',
        HelpLink: '帮助',
        UserName: '用户名:',
        Password: '密码:',
        RememberMe: '下次记住我',
        ForgotPassword: '忘记密码？',
        SignUp: '现在就注册',
        LoginButton: '登录',
        MyAccount: '我的帐户',
        LogoutLink: '登出',
        HelpCloseButton: '关闭',
        HelpFullScreenButton: '全屏',
        UserIdle: '你还在吗？请重新登录。',
        History: '历史',
        Permalink: '固定链接',
        AddToFavorites: '添加到收藏夹',
        RotateHistory: '旋转',
        Welcome: '欢迎 <b>{0}</b>, 今天是 {1:D}',
        ChangeLanguageToolTip: '改变你的语言',
        PermalinkToolTip: '选定的记录创建一个永久链接',
        HistoryToolTip: '查看历史以前选定的记录',
        AutoDetectLanguageOption: 'Auto Detect'
    };

    _mr.Messages = {
        InvalidUserNameAndPassword: '您的用户名和密码无效。',
        BlankUserName: '用户名不能为空。',
        BlankPassword: '密码不能为空。',
        PermalinkUnavailable: '固定链接不可用。请选择一个记录。',
        HistoryUnavailable: '历史是不可用。'
    };

    _mr.Manager = {
        UsersTab: '用户',
        RolesTab: '角色',
        UsersInRole: '在角色的用户'
    };

    if (typeof Sys !== 'undefined') Sys.Application.notifyScriptLoaded();
})();