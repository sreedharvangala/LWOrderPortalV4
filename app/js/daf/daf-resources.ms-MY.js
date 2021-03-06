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
        ItemsPerPage: 'Entri per halaman:',
        PageSizes: [10, 15, 20, 25],
        ShowingItems: 'Mempamerkan \u003cb\u003e{0}\u003c/b\u003e-\u003cb\u003e{1}\u003c/b\u003e item \u003cb\u003e{2}\u003c/b\u003e',
        SelectionInfo: ' (<b>{0}</b> selected)',
        Refresh: 'Refresh',
        Next: 'Seterusnya »',
        Previous: '« Sebelum',
        Page: 'Page',
        PageButtonCount: 10
    };

    _dvr.ActionBar = {
        View: 'Lihat'
    };

    _dvr.ModalPopup = {
        Close: 'Tutup',
        MaxWidth: 800,
        MaxHeight: 600,
        OkButton: 'OK',
        CancelButton: 'Batal',
        SaveButton: 'Simpan',
        SaveAndNewButton: 'Simpan dan New'
    };

    _dvr.Menu = {
        SiteActions: 'Laman Tindakan',
        SeeAlso: 'See Also',
        Summary: 'Ringkasan',
        Tasks: 'Tugas',
        About: 'Kira-kira'
    };

    _dvr.HeaderFilter = {
        GenericSortAscending: 'Terkecil di Top',
        GenericSortDescending: 'Terbesar di Top',
        StringSortAscending: 'Menaik',
        StringSortDescending: 'Menurun',
        DateSortAscending: 'Terawal di Top',
        DateSortDescending: 'Terbaru pada Top',
        EmptyValue: '(Kosong)',
        BlankValue: '(Kosong)',
        Loading: 'Loading ...',
        ClearFilter: 'Penapis jelas dari {0}',
        SortBy: 'Sort oleh {0}',
        MaxSampleTextLen: 80,
        CustomFilterOption: 'Penapis ...'
    };

    _dvr.InfoBar = {
        FilterApplied: 'Penapis telah digunakan.',
        ValueIs: ' <span class="Highlight">{0}</span> ',
        Or: ' atau ',
        And: ' dan ',
        EqualTo: 'adalah sama dengan ',
        LessThan: 'adalah kurang daripada ',
        LessThanOrEqual: 'kurang daripada atau sama dengan ',
        GreaterThan: 'adalah lebih besar daripada ',
        GreaterThanOrEqual: 'adalah lebih besar daripada atau sama dengan ',
        Like: 'adalah seperti ',
        StartsWith: 'bermula dengan ',
        Empty: 'kosong',
        QuickFind: ' Mana-mana bidang mengandungi '
    };

    _dvr.Lookup = {
        SelectToolTip: 'Pilih {0}',
        ClearToolTip: '{0} Jelas',
        NewToolTip: 'New {0}',
        SelectLink: '(Pilih)',
        ShowActionBar: true,
        DetailsToolTip: 'Lihat butiran untuk {0}',
        ShowDetailsInPopup: true,
        GenericNewToolTip: 'Cipta Baru',
        AddItem: 'Tambah Item'
    };

    _dvr.Validator = {
        Required: 'Diperlukan',
        RequiredField: 'Medan ini dikehendaki.',
        EnforceRequiredFieldsWithDefaultValue: false,
        NumberIsExpected: 'Nombor dijangka.',
        BooleanIsExpected: 'Nilai logik dijangka.',
        DateIsExpected: 'Tarikh dijangka.',
        Optional: 'pilihan'
    };

    var _mn = Sys.CultureInfo.CurrentCulture.dateTimeFormat.MonthNames;

    _dvr.Data = {
        ConnectionLost: 'Sambungan rangkaian telah hilang. Cuba lagi?',
        AnyValue: '(Mana-mana)',
        NullValue: '<span class="NA">n / a</span>',
        NullValueInForms: 'N / A',
        BooleanDefaultStyle: 'DropDownList',
        BooleanOptionalDefaultItems: [[null, 'N / A'], [false, 'Tiada'], [true, 'Ya']],
        BooleanDefaultItems: [[false, 'Tiada'], [true, 'Ya']],
        MaxReadOnlyStringLen: 600,
        NoRecords: 'Tiada rekod dijumpai.',
        BlobHandler: 'Blob.ashx',
        BlobDownloadLink: 'memuat turun',
        BlobDownloadLinkReadOnly: '<span style="color:gray;">memuat turun</span>',
        BlobDownloadHint: 'Klik di sini untuk memuat turun fail asal.',
        BlobDownloadOriginalHint: 'Ketik imej untuk memuat turun fail asal.',
        BatchUpdate: 'mengemas kini',
        NoteEditLabel: 'mengedit',
        NoteDeleteLabel: 'memadam',
        NoteDeleteConfirm: 'Padam?',
        UseLEV: 'Tampal \u0026quot;{0}\u0026quot;',
        DiscardChanges: 'Membuang perubahan?',
        KeepOriginalSel: 'pastikan pemilihan asal',
        DeleteOriginalSel: 'padam pemilihan asal',
        Import: {
            UploadInstruction: 'Sila pilih fail untuk dimuat naik. Fail mesti dalam \u003cb\u003eCSV\u003c/b\u003e, format \u003cb\u003eXLS\u003c/b\u003e, atau \u003cb\u003eXLSX\u003c/b\u003e.',
            DownloadTemplate: 'Muat turun fail templat import.',
            Uploading: 'Fail anda sedang dimuat naik ke pelayan. Sila tunggu ...',
            MappingInstruction: 'Terdapat rekod \u003cb\u003e{0}\u003c/b\u003e (s) dalam \u003cb\u003e{1}\u003c/b\u003e fail bersedia untuk processed.\u003cbr/\u003ePlease peta bidang import dari fail data untuk bidang permohonan dan klik \u003ci\u003eImport\u003c/i\u003e untuk memulakan pemprosesan.',
            StartButton: 'Import',
            AutoDetect: '(Auto mengesan)',
            Processing: 'Import pemprosesan fail telah dimulakan. Data yang diimport rekod akan menjadi disediakan di atas pemprosesan berjaya. Anda mungkin perlu untuk menyegarkan pandangan / muka surat yang berkaitan untuk melihat rekod-rekod yang diimport.',
            Email: 'Hantar log import kepada alamat e-mel berikut (pilihan)',
            EmailNotSpecified: 'Penerima log import telah tidak dinyatakan. Teruskan anyway?'
        },
        Filters: {
            Labels: {
                And: 'dan',
                Or: 'atau',
                Equals: 'sama dengan',
                Clear: 'Jelas',
                SelectAll: '(Select All)',
                Includes: 'termasuk',
                FilterToolTip: 'Perubahan'
            },
            Number: {
                Text: 'Bilangan Penapis',
                Kind: 'Nombor',
                List: [
                    { Function: '=', Text: 'Sama dengan', Prompt: true },
                    { Function: '<>', Text: 'Tidak Sama', Prompt: true },
                    { Function: '<', Text: 'Kurang', Prompt: true },
                    { Function: '>', Text: 'Lebih besar daripada', Prompt: true },
                    { Function: '<=', Text: 'Kurang Atau Sama', Prompt: true },
                    { Function: '>=', Text: 'Lebih besar daripada atau sama', Prompt: true },
                    { Function: '$between', Text: 'Antara', Prompt: true },
                    { Function: '$in', Text: 'Termasuk', Prompt: true, Hidden: true },
                    { Function: '$notin', Text: 'Tidak termasuk', Prompt: true, Hidden: true },
                    { Function: '$isnotempty', Text: 'Tidak kosong' },
                    { Function: '$isempty', Text: 'Kosong' }
                ]
            },
            Text: {
                Text: 'Teks Penapis',
                Kind: 'Teks',
                List: [
                    { Function: '=', Text: 'Sama dengan', Prompt: true },
                    { Function: '<>', Text: 'Tidak Sama', Prompt: true },
                    { Function: '$beginswith', Text: 'Bermula Dengan', Prompt: true },
                    { Function: '$doesnotbeginwith', Text: 'Tidak Bermula Dengan', Prompt: true },
                    { Function: '$contains', Text: 'Mengandungi', Prompt: true },
                    { Function: '$doesnotcontain', Text: 'Tidak Mengandungi', Prompt: true },
                    { Function: '$endswith', Text: 'Berakhir Dengan', Prompt: true },
                    { Function: '$doesnotendwith', Text: 'Tidak berakhir dengan', Prompt: true },
                    { Function: '$in', Text: 'Termasuk', Prompt: true, Hidden: true },
                    { Function: '$notin', Text: 'Tidak termasuk', Prompt: true, Hidden: true },
                    { Function: '$isnotempty', Text: 'Tidak kosong' },
                    { Function: '$isempty', Text: 'Kosong' }
                ]
            },
            Boolean: {
                Text: 'Penapis logik',
                Kind: 'Logik',
                List: [
                    { Function: '$true', Text: 'Ya' },
                    { Function: '$false', Text: 'Tiada' },
                    { Function: '$isnotempty', Text: 'Tidak kosong' },
                    { Function: '$isempty', Text: 'Kosong' }
                ]
            },
            Date: {
                Text: 'Tarikh Penapis',
                Kind: 'Tarikh',
                List: [
                    { Function: '=', Text: 'Sama dengan', Prompt: true },
                    { Function: '<>', Text: 'Tidak Sama', Prompt: true },
                    { Function: '<', Text: 'Sebelum', Prompt: true },
                    { Function: '>', Text: 'Selepas', Prompt: true },
                    { Function: '<=', Text: 'Pada atau Sebelum', Prompt: true },
                    { Function: '>=', Text: 'Pada atau Selepas', Prompt: true },
                    { Function: '$between', Text: 'Antara', Prompt: true },
                    { Function: '$in', Text: 'Termasuk', Prompt: true, Hidden: true },
                    { Function: '$notin', Text: 'Tidak termasuk', Prompt: true, Hidden: true },
                    { Function: '$isnotempty', Text: 'Tidak kosong' },
                    { Function: '$isempty', Text: 'Kosong' },
                    null,
                    { Function: '$tomorrow', Text: 'Esok' },
                    { Function: '$today', Text: 'Hari ini' },
                    { Function: '$yesterday', Text: 'Semalam' },
                    null,
                    { Function: '$nextweek', Text: 'Minggu Seterusnya' },
                    { Function: '$thisweek', Text: 'Minggu Ini' },
                    { Function: '$lastweek', Text: 'Minggu Lepas' },
                    null,
                    { Function: '$nextmonth', Text: 'Bulan Depan' },
                    { Function: '$thismonth', Text: 'Bulan ini' },
                    { Function: '$lastmonth', Text: 'Bulan lepas' },
                    null,
                    { Function: '$nextquarter', Text: 'Suku Seterusnya' },
                    { Function: '$thisquarter', Text: 'Suku ini' },
                    { Function: '$lastquarter', Text: 'Suku terakhir' },
                    null,
                    { Function: '$nextyear', Text: 'Tahun Depan' },
                    { Function: '$thisyear', Text: 'Tahun Ini' },
                    { Function: '$yeartodate', Text: 'Tahun kepada Tarikh' },
                    { Function: '$lastyear', Text: 'Tahun Lepas' },
                    null,
                    { Function: '$past', Text: 'Masa lampau' },
                    { Function: '$future', Text: 'Masa Depan' },
                    null,
                    {
                        Text: 'Semua Tarikh dalam Tempoh',
                        List: [
                            { Function: '$quarter1', Text: 'Suku 1' },
                            { Function: '$quarter2', Text: 'Quarter 2' },
                            { Function: '$quarter3', Text: 'Suku 3' },
                            { Function: '$quarter4', Text: 'Suku 4' },
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
        RequiredFiledMarkerFootnote: '* - menunjukkan medan yang diperlukan',
        SingleButtonRowFieldLimit: 7,
        GeneralTabText: 'Ketua',
        Minimize: 'Runtuh',
        Maximize: 'Expand'
    };

    _dvr.Grid = {
        InPlaceEditContextMenuEnabled: true,
        QuickFindText: 'Carian Pantas',
        QuickFindToolTip: 'Jenis untuk mencari rekod dan tekan Enter',
        ShowAdvancedSearch: 'Papar Advanced Search',
        VisibleSearchBarFields: 3,
        DeleteSearchBarField: '(Padam)',
        //AddSearchBarField: 'Lagi Search Medan',
        HideAdvancedSearch: 'Sembunyikan Advanced Search',
        PerformAdvancedSearch: 'Search',
        ResetAdvancedSearch: 'Reset',
        NewRowLink: 'Klik di sini untuk membuat rekod baru.',
        RootNodeText: 'Top Level',
        FlatTreeToggle: 'Tukar kepada Hierarki',
        HierarchyTreeModeToggle: 'Tukar ke Senarai Flat',
        AddConditionText: 'Tambah keadaan carian',
        AddCondition: 'Tambah Keadaan',
        RemoveCondition: 'Buang Keadaan',
        ActionColumnHeaderText: 'Tindakan',
        Aggregates: {
            None: { FmtStr: '', ToolTip: '' },
            Sum: { FmtStr: 'Jumlah: {0}', ToolTip: 'Jumlah-{0}' },
            Count: { FmtStr: 'Count: {0}', ToolTip: 'Bilang {0}' },
            Avg: { FmtStr: 'Purata: {0}', ToolTip: 'Purata daripada {0}' },
            Max: { FmtStr: 'Max: {0}', ToolTip: 'Maksimum-{0}' },
            Min: { FmtStr: 'Min: {0}', ToolTip: 'Minimum-{0}' }
        },
        Freeze: 'Membekukan',
        Unfreeze: 'Unfreeze'
    };

    _dvr.Views = {
        DefaultDescriptions: {
            '$DefaultGridViewDescription': 'Ini adalah senarai {0}.',
            '$DefaultEditViewDescription': 'Sila mengkaji maklumat {0} di bawah. Klik Edit untuk ubah rekod ini, klik Padam untuk memadam rekod, atau klik Batal / Close untuk kembali semula.',
            '$DefaultCreateViewDescription': 'Sila mengisi borang ini dan klik butang OK untuk mencipta satu rekod {0} baru. Klik Batal untuk kembali ke skrin sebelumnya.'
        },
        DefaultCategoryDescriptions: {
            '$DefaultEditDescription': 'Ini adalah bidang rekod {0} yang boleh disunting.',
            '$DefaultNewDescription': 'Form dengan lengkap. Pastikan untuk memasuki semua bidang yang diperlukan.',
            '$DefaultReferenceDescription': 'Butir-butir tambahan kira-kira {0} yang diperuntukkan dalam seksyen maklumat rujukan.'
        }
    };

    _dvr.Actions = {
        Scopes: {
            'Grid': {
                'Select': {
                    HeaderText: 'Pilih'
                },
                'Edit': {
                    HeaderText: 'Edit'
                },
                'Delete': {
                    HeaderText: 'Padam',
                    Confirmation: 'Padam?',
                    Notify: '{$ selected} dipadamkan'
                },
                'Duplicate': {
                    HeaderText: 'Pendua'
                },
                'New': {
                    HeaderText: 'Baru'
                },
                'BatchEdit': {
                    HeaderText: 'Batch Edit'
                    //                    ,CommandArgument: {
                    //                        'editForm1': {
                    //                            HeaderText: 'Edit kumpulan (Borang)'
                    //                        }
                    //                    }
                },
                'Open': {
                    HeaderText: 'Terbuka'
                }
            },
            'Form': {
                'Edit': {
                    HeaderText: 'Edit'
                },
                'Delete': {
                    HeaderText: 'Padam',
                    Confirmation: 'Padam?',
                    Notify: '{$ selected} dipadamkan'
                },
                'Cancel': {
                    HeaderText: 'Tutup',
                    WhenLastCommandName: {
                        'Duplicate': {
                            HeaderText: 'Batal'
                        },
                        'Edit': {
                            HeaderText: 'Batal'
                        },
                        'New': {
                            HeaderText: 'Batal'
                        }

                    }
                },
                'Update': {
                    HeaderText: 'OK',
                    Notify: 'Disimpan - {0}',
                    CommandArgument: {
                        'Save': {
                            HeaderText: 'Simpan',
                            Notify: 'Disimpan - {0}'
                        }
                    },
                    WhenLastCommandName: {
                        'BatchEdit': {
                            HeaderText: 'Pemilihan Update',
                            Confirmation: 'Update?',
                            Notify: 'Disimpan - {0}'
                        }
                    }
                },
                'Insert': {
                    HeaderText: 'OK',
                    Notify: 'Disimpan - {0}',
                    CommandArgument: {
                        'Save': {
                            HeaderText: 'Simpan',
                            Notify: 'Disimpan - {0}'
                        },
                        'SaveAndNew': {
                            HeaderText: 'Simpan dan New',
                            Notify: 'Disimpan - {0}'
                        }
                    }
                },
                'Confirm': {
                    HeaderText: 'OK'
                },
                'Navigate': {
                    Controller: {
                        'SiteContent': {
                            HeaderText: 'Add Sistem Pengenalan'
                        }
                    }
                }
            },
            'ActionBar': {
                _Self: {
                    'Actions': {
                        HeaderText: 'Tindakan'
                    },
                    'Report': {
                        HeaderText: 'Laporan'
                    },
                    'Record': {
                        HeaderText: 'Rekod'
                    }
                },
                'New': {
                    HeaderText: 'New {0}',
                    Description: 'Buat rekod {0} baru.',
                    HeaderText2: 'Baru',
                    VarMaxLen: 15
                },
                'Edit': {
                    HeaderText: 'Edit'
                },
                'Delete': {
                    HeaderText: 'Padam',
                    Confirmation: 'Padam?',
                    Notify: '{$ selected} dipadamkan'
                },
                'ExportCsv': {
                    HeaderText: 'Muat turun',
                    Description: 'Barangan-turun dalam format CSV.'
                },
                'ExportRowset': {
                    HeaderText: 'Eksport ke spreadsheet',
                    Description: 'Menganalisis item dengan spreadsheet\u003cbr/\u003eapplication.'
                },
                'ExportRss': {
                    HeaderText: 'Lihat RSS Feed',
                    Description: 'Item sindiket dengan pembaca RSS.'
                },
                'Import': {
                    HeaderText: 'Import Dari Fail',
                    Description: 'Upload CSV, XLS, atau rekod import XLSX file\u003cbr/\u003eto.'
                },
                'Update': {
                    HeaderText: 'Simpan',
                    Description: 'Simpan perubahan kepada pangkalan data.',
                    Notify: 'Disimpan - {0}'
                },
                'Insert': {
                    HeaderText: 'Simpan',
                    Description: 'Simpan rekod baru untuk pangkalan data.',
                    Notify: 'Disimpan - {0}'
                },
                'Cancel': {
                    HeaderText: 'Batal',
                    WhenLastCommandName: {
                        'Edit': {
                            HeaderText: 'Batal',
                            Description: 'Batal semua perubahan rekod.'
                        },
                        'New': {
                            HeaderText: 'Batal',
                            Description: 'Membatalkan rekod baru.'
                        }
                    }
                },
                'Report': {
                    HeaderText: 'Laporan',
                    Description: 'Menyebabkan laporan dalam format PDF'
                },
                'ReportAsPdf': {
                    HeaderText: 'Dokumen PDF',
                    Description: 'Lihat item Adobe PDF document.\u003cbr/\u003eRequires pembaca yang serasi.'
                },
                'ReportAsImage': {
                    HeaderText: 'Imej Multipage',
                    Description: 'Lihat item sebagai imej TIFF multipage.'
                },
                'ReportAsExcel': {
                    HeaderText: 'Spreadsheet',
                    Description: 'Lihat item dalam spreadsheet Excel formatted\u003cbr/\u003eMicrosoft.'
                },
                'ReportAsWord': {
                    HeaderText: 'Microsoft Word',
                    Description: 'Lihat item dalam dokumen Word formatted\u003cbr/\u003eMicrosoft.'
                },
                'DataSheet': {
                    HeaderText: 'Tunjukkan dalam Risalah Data',
                    Description: 'Paparan barangan menggunakan sheet\u003cbr/\u003eformat data.'
                },
                'Grid': {
                    HeaderText: 'Tunjuk biasa Lihat',
                    Description: 'Paparan barangan dalam format standard\u003cbr/\u003elist.'
                },
                'Tree': {
                    HeaderText: 'Show Hierarki',
                    Description: 'Paparan hubungan hierarki.'
                },
                'Search': {
                    HeaderText: 'Search',
                    Description: 'Search {0}'
                },
                'Upload': {
                    HeaderText: 'Muat naik',
                    Description: 'Muat naik berbilang fail.'
                }
            },
            'Row': {
                'Update': {
                    HeaderText: 'Simpan',
                    Notify: 'Disimpan - {0}',
                    WhenLastCommandName: {
                        'BatchEdit': {
                            HeaderText: 'Pemilihan Update',
                            Confirmation: 'Update?'
                        }
                    }
                },
                'Insert': {
                    HeaderText: 'Masukkan',
                    Notify: 'Disimpan - {0}'
                },
                'Cancel': {
                    HeaderText: 'Batal'
                }
            },
            'ActionColumn': {
                'Edit': {
                    HeaderText: 'Edit'
                },
                'Delete': {
                    HeaderText: 'Padam',
                    Confirmation: 'Padam?',
                    Notify: '{$ selected} dipadamkan'
                }
            }
        }
    };

    _dvr.Editor = {
        Undo: 'Buat asal',
        Redo: 'Buat semula',
        Bold: 'Berani',
        Italic: 'Italic',
        Underline: 'Garis',
        Strikethrough: 'Menyerang Melalui',
        Subscript: 'Sub Script',
        Superscript: 'Super Script',
        JustifyLeft: 'Jelaskan Kiri',
        JustifyCenter: 'Pusat mewajarkan',
        JustifyRight: 'Jelaskan Hak',
        JustifyFull: 'Jelaskan penuh',
        InsertOrderedList: 'Masukkan Senarai Tertib',
        InsertUnorderedList: 'Masukkan Senarai Tidak Tertib',
        CreateLink: 'Membuat Link',
        UnLink: 'Hapuskan',
        RemoveFormat: 'Buang Format',
        SelectAll: 'Pilih Semua',
        UnSelect: 'Jangan pilih',
        Delete: 'Padam',
        Cut: 'Potong',
        Copy: 'Menyalin',
        Paste: 'Tampal',
        BackColor: 'Warna Kembali',
        ForeColor: 'Warna hadapan',
        FontName: 'Nama Font',
        FontSize: 'Saiz Font',
        Indent: 'Inden',
        Outdent: 'Outdent',
        InsertHorizontalRule: 'Masukkan Baris Mendatar',
        HorizontalSeparator: 'Separator',
        Format: 'Format',
        FormatBlock: {
            p: 'Perenggan',
            blockquote: 'Sebutharga',
            h1: 'Tajuk 1',
            h2: 'Tajuk 2',
            h3: 'Tajuk 3',
            h4: 'Tajuk 4',
            h5: 'Tajuk 5',
            h6: 'Tajuk 6'
        },
        Rtf: {
            editor: 'Skrin penuh'
        }
    };

    _dvr.Mobile = {
        UpOneLevel: 'Sehingga Satu Tahap',
        Back: 'Pulangan',
        BatchEdited: '{0} dikemas kini',
        Sort: 'Mengisih',
        Sorted: 'Diisih',
        SortedDefault: 'Pesanan jenis lalai.',
        SortByField: 'Pilih medan untuk mengubah perintah menyusun daripada \u003cb\u003e{0}\u003c/b\u003e.',
        SortByOptions: 'Pilih untuk menyusun daripada \u003cb\u003e{0}\u003c/b\u003e oleh medan \u003cb\u003e{1}\u003c/b\u003e dalam senarai pilihan di bawah.',
        DefaultOption: 'Piawai',
        Auto: 'Auto',
        Filter: 'Penapis',
        List: 'Senarai',
        Cards: 'Kad',
        Grid: 'Jajaran',
        Map: 'Peta',
        Calendar: 'Kalendar',
        ZoomIn: 'Zum masuk',
        ZoomOut: 'Zum keluar',
        Directions: 'Arahan',
        AlternativeView: 'Pilih pandangan alternatif data.',
        PresentationStyle: 'Pilih gaya persembahan data.',
        LookupViewAction: 'Lihat',
        LookupSelectAction: 'Pilih',
        LookupClearAction: 'Jelas',
        LookupNewAction: 'Baru',
        LookupInstruction: 'Sila pilih \u003cb\u003e{0}\u003c/b\u003e dalam senarai.',
        LookupOriginalSelection: 'Pemilihan asal adalah \u003cb\u003e\u0026quot;{0}\u0026quot;\u003c/b\u003e.',
        EmptyContext: 'Tindakan tidak boleh didapati.',
        Favorites: 'Kegemaran',
        History: 'Sejarah',
        FilterSiteMap: 'Peta tapak penapis ...',
        ResumeLookup: 'Resume Lookup',
        ResumeEntering: 'Resume Memasuki',
        ResumeEditing: 'Resume Penyuntingan',
        ResumeBrowsing: 'Resume Browsing',
        ResumeViewing: 'Resume Melihat',
        Menu: 'Menu',
        Home: 'Laman Utama',
        Settings: 'Tetapan',
        Sidebar: 'Sidebar',
        Landscape: 'Pemandangan',
        Portrait: 'Potret',
        Never: 'Tak pernah',
        Always: 'Selalu',
        ShowSystemButtons: 'Butang Show Sistem',
        OnHover: 'dalam Hover',
        ButtonShapes: 'Bentuk butang',
        PromoteActions: 'Promosikan Tindakan',
        ConfirmReload: 'Muat semula?',
        ClearText: 'Teks yang jelas',
        SeeAll: 'Lihat Semua',
        More: 'Lebih',
        TouchUINotSupported: 'Touch UI tidak disokong dalam pelayar ini.',
        ShowingItemsInfo: 'Menunjukkan {0} item.',
        FilterByField: 'Pilih medan untuk memohon penapis untuk \u003cb\u003e{0}\u003c/b\u003e.',
        Apply: 'Memohon',
        FilterByOptions: 'Pilih satu atau lebih pilihan dalam senarai di bawah dan tekan \u003cb\u003e{2}\u003c/b\u003e untuk menapis \u003cb\u003e{0}\u003c/b\u003e oleh medan \u003cb\u003e{1}\u003c/b\u003e.',
        Suggestions: 'Cadangan',
        UnSelect: 'Jangan pilih',
        AdvancedSearch: 'Carian Terperinci',
        QuickFindScope: 'Cari dalam ...',
        QuickFindDescription: 'Carian dalam {0}',
        AddMatchingGroup: 'Tambah kumpulan yang hampir',
        MatchAll: 'Padankan semua keadaan',
        MatchAny: 'Padankan mana-mana syarat',
        DoNotMatchAll: 'Tidak sesuai dengan semua keadaan',
        DoNotMatchAny: 'Tidak sepadan dengan mana-mana syarat',
        MatchAllPastTense: 'Dipadankan semua keadaan',
        MatchAnyPastTense: 'Dipadankan apa-apa syarat',
        DoNotMatchAllPastTense: 'Tidak sepadan dengan semua keadaan',
        DoNotMatchAnyPastTense: 'Tidak sepadan dengan mana-mana syarat',
        In: 'dalam',
        Recent: 'Baru-baru ini',
        Matched: 'Dipadankan',
        DidNotMatch: 'Tidak sepadan',
        ClearFilter: 'Jelas Filter',
        ResetSearchConfirm: 'Tetapkan semula syarat carian?',
        FilterCleared: 'Membersihkan semua penapis.',
        AdvancedSearchInstruction: 'Masukkan syarat yang mesti dipadankan dan tekan butang carian.',
        Refreshed: 'Refresh',
        Group: 'Kumpulan',
        Grouped: 'Dikelompokkan',
        UnGrouped: 'Pengumpulan telah dialih keluar',
        GroupedBy: 'Dikumpulkan oleh',
        GroupByField: 'Pilih medan untuk kumpulan \u003cb\u003e{0}\u003c/b\u003e.',
        Show: 'Tunjuk',
        Hide: 'Sembunyi',
        None: 'Tiada',
        Next: 'Seterusnya',
        Prev: 'Terdahulu',
        FitToWidth: 'Fit To Lebar',
        MultiSelection: 'Multi Pemilihan',
        InlineEditing: 'Penyuntingan dalam talian',
        ItemsSelectedOne: '{0} Item yang dipilih',
        ItemsSelectedMany: '{0} Item yang dipilih',
        TypeToSearch: 'Taip untuk Mencari',
        NoMatches: 'Tiada padanan.',
        ShowingItemsRange: 'Menunjukkan {0} {1} daripada barangan',
        Finish: 'Selesai',
        ShowOptions: 'Tunjukkan Pilihan',
        ConfirmContinue: 'Teruskan?',
        AddAccount: 'Tambah akaun',
        Fullscreen: 'Skrin penuh',
        ExitFullscreen: 'Exit Fullscreen',
        Apps: 'Apps',
        Forget: 'Lupakan',
        ManageAccounts: 'Urus Akaun',
        SignedOut: 'Log Keluar',
        Submit: 'hantar',
        Error: 'ralat',
        Line: 'Line',
        Download: 'muat turun',
        Orientation: 'orientasi',
        Device: 'peranti',
        ShowMore: 'Tunjukkan Lagi',
        ShowLess: 'persembahan Kurang',
        WithSpecifiedFilters: 'Dengan Penapis Tertentu',
        WithSelectedValues5: 'Dengan Nilai Terpilih (Top 5)',
        WithSelectedValues10: 'Dengan Nilai Terpilih (Top 10)',
        ReadOnly: '{0} adalah baca sahaja.',
        InlineCommands: {
            List: {
                Select: 'Pilih barang',
                Edit: 'Edit Item',
                New: 'Item baru',
                Duplicate: 'Item pendua',
            },
            Grid: {
                Select: 'Pilih Row',
                Edit: 'Edit Row',
                New: 'Barisan baru',
                Duplicate: 'Baris Duplicate',
            }
        },
        DisplayDensity: {
            Label: 'Paparan Ketumpatan',
            List: {
                Tiny: 'Tiny',
                Condensed: 'Pekat',
                Compact: 'Padat',
                Comfortable: 'Selesa'
            }
        },
        Files: {
            KB: 'KB',
            MB: 'KB',
            Bytes: 'bytes',
            Drop: 'Letakkan fail di sini',
            DropMany: 'Lepaskan fail di sini',
            Tap: 'Ketik untuk pilih fail',
            TapMany: 'Ketik untuk memilih fail',
            Click: 'Klik untuk pilih fail',
            ClickMany: 'Klik untuk memilih fail',
            Clear: 'Jelas',
            ClearConfirm: 'Yang jelas nyata?',
            Sign: 'Daftar di sini',
            Cleared: 'Nilai akan dibersihkan pada simpanan'
        },
        Import: {
            SelectFile: 'Pilih fail data dalam CSV, XLS, XLSX atau format.',
            NotSupported: 'format data \u0026quot;{0}\u0026quot; tidak disokong.',
            NotMatched: '(Tidak sepadan)',
            FileStats: 'Terdapat rekod \u003cb\u003e{0}\u003c/b\u003e dalam fail \u003cb\u003e{1}\u003c/b\u003e bersedia untuk diproses. Sila sepadan dengan nama lapangan.',
            Importing: 'mengimport',
            Into: 'ke dalam',
            StartImport: 'Mula Import',
            InsertingRecords: 'memasukkan rekod',
            TestingRecords: 'rekod ujian',
            ResolvingReferences: 'menyelesaikan rujukan',
            Complete: 'lengkap',
            Expected: 'Dijangka siap',
            Remaining: 'Dijangka siap',
            Done: 'mengimport Bidang',
            Duplicates: 'pendua'
        },
        Themes: {
            Label: 'Tema',
            Accent: 'Accent',
            List: {
                None: 'Tiada',
                Light: 'Cahaya',
                Dark: 'Gelap',
                Aquarium: 'Akuarium',
                Azure: 'Azure',
                Belltown: 'Belltown',
                Berry: 'Berry',
                Bittersweet: 'Pahit manis',
                Cay: 'Cay',
                Citrus: 'Citrus',
                Classic: 'Classic',
                Construct: 'Membina',
                Convention: 'Konvensyen',
                DarkKnight: 'Dark Knight',
                Felt: 'Merasa',
                Graham: 'Graham',
                Granite: 'Granite',
                Grapello: 'Grapello',
                Gravity: 'Graviti',
                Lacquer: 'Lacquer',
                Laminate: 'Lamina',
                Lichen: 'Liken',
                Mission: 'Misi',
                Modern: 'Moden',
                ModernRose: 'Rose Moden',
                Municipal: 'Perbandaran',
                Petal: 'Petal',
                Pinnate: 'Pinat',
                Plastic: 'Plastik',
                Ricasso: 'Ricasso',
                Simple: 'Mudah',
                Social: 'Sosial',
                Summer: 'Musim panas',
                Vantage: 'Vantage',
                Verdant: 'Menghijau',
                Viewpoint: 'Viewpoint',
                WhiteSmoke: 'Asap putih',
                Yoshi: 'Yoshi'
            }
        },
        Transitions: {
            Label: 'Peralihan',
            List: {
                none: 'Tiada',
                slide: 'Slide',
                fade: 'Fade',
                pop: 'Pop',
                flip: 'Flip',
                turn: 'Seterusnya',
                flow: 'Aliran',
                slideup: 'Slide Up',
                slidedown: 'Slide Down'
            }
        },
        LabelsInList: {
            Label: 'Labels In List',
            List: {
                DisplayedAbove: 'Dipaparkan atas',
                DisplayedBelow: 'Di bawah dipaparkan'
            }
        },
        InitialListMode: {
            Label: 'Mod Senarai awal',
            List: {
                SeeAll: 'Lihat Semua',
                Summary: 'Ringkasan'
            }
        },
        Dates: {
            SmartDates: 'Tarikh Smart',
            Yesterday: 'semalam',
            Last: 'lepas',
            Today: 'hari ini',
            OneHour: 'satu jam yang lalu',
            MinAgo: '{0} Min yang lalu',
            AMinAgo: 'minit yang lalu',
            InHour: 'dalam masa sejam',
            InMin: 'dalam min {0}',
            InAMin: 'dalam satu minit',
            Now: 'sekarang',
            JustNow: 'tadi',
            Tomorrow: 'esok',
            Next: 'Seterusnya'
        },
        Sync: {
            Uploading: 'Memuat naik {0} ...'
        },
        Develop: {
            Tools: 'Alat Pembangun',
            Explorer: 'projek Explorer',
            FormLayout: 'borang Layout',
            FormLayoutInstr: 'saiz skrin pilih untuk dimasukkan ke dalam susun atur.'
        }
    };

    _dvr.Device = {
        Exit: 'Keluar',
        DeviceLoginPrompt: 'Sila log masuk untuk membenarkan akses pada peranti ini.'
    };

    _dvr.Presenters = {
        Charts: {
            Text: 'Carta',
            DataWarning: 'Bilangan maksimum perkara untuk proses adalah {0: d}. Klik di sini untuk menapis keputusan.',
            ShowData: 'Show Data',
            ShowChart: 'Show Carta',
            Sizes: {
                Label: 'Saiz',
                Small: 'Kecil',
                Medium: 'Sederhana',
                Large: 'Besar'
            },
            ChartLabels: {
                By: 'oleh',
                Top: 'atas',
                Other: 'Lain-lain',
                Blank: 'Blank',
                GrandTotals: 'Grand Total',
                CountOf: 'Bilang',
                SumOf: 'Jumlah',
                AvgOf: 'Purata',
                MinOf: 'Minimum',
                MaxOf: 'Maksimum',
                Quarter: 'Suku',
                Week: 'Minggu'
            }
        },
        Calendar: {
            Text: 'Kalendar',
            Today: 'Hari ini',
            Noon: 'Tengah hari',
            Year: 'Tahun',
            Month: 'Bulan',
            Week: 'Minggu',
            Day: 'Hari',
            Agenda: 'Agenda',
            Sync: 'Sync',
            Less: 'Kurang'
        }
    };

    // membership resources

    var _mr = Web.MembershipResources = {};

    _mr.Bar = {
        LoginLink: 'Masuk',
        LoginText: ' ke laman web ini',
        HelpLink: 'Bantuan',
        UserName: 'Nama Pengguna:',
        Password: 'Kata laluan:',
        RememberMe: 'Ingat saya pada masa akan datang',
        ForgotPassword: 'Lupa kata laluan anda?',
        SignUp: 'Sign up sekarang',
        LoginButton: 'Masuk',
        MyAccount: 'Akaun Saya',
        LogoutLink: 'Logout',
        HelpCloseButton: 'Tutup',
        HelpFullScreenButton: 'Skrin Penuh',
        UserIdle: 'Adakah anda masih ada? Sila login sekali lagi.',
        History: 'Sejarah',
        Permalink: 'Permalink',
        AddToFavorites: 'Tambah pada Kegemaran',
        RotateHistory: 'Putar',
        Welcome: 'Selamat Datang <b>{0}</b>, Hari ini adalah {1:D}',
        ChangeLanguageToolTip: 'Tukar bahasa anda',
        PermalinkToolTip: 'Buat link tetap untuk rekod yang dipilih',
        HistoryToolTip: 'Sejarah Lihat rekod yang dipilih sebelum ini',
        AutoDetectLanguageOption: 'Auto Detect'
    };

    _mr.Messages = {
        InvalidUserNameAndPassword: 'Nama pengguna dan kata laluan tidak sah.',
        BlankUserName: 'Nama pengguna tidak boleh kosong.',
        BlankPassword: 'Password tidak boleh kosong.',
        PermalinkUnavailable: 'Permalink tidak boleh didapati. Sila pilih rekod.',
        HistoryUnavailable: 'Sejarah tidak boleh didapati.'
    };

    _mr.Manager = {
        UsersTab: 'Pengguna',
        RolesTab: 'Peranan',
        UsersInRole: 'Pengguna dalam Peranan'
    };

    if (typeof Sys !== 'undefined') Sys.Application.notifyScriptLoaded();
})();