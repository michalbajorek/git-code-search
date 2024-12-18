﻿using GitCodeSearch.Search.Result;
using System.Windows;
using System.Windows.Controls;

namespace GitCodeSearch.Views;

public class SearchTemplateSelector : DataTemplateSelector
{
    public DataTemplate? FileContentTemplate { get; set; }

    public DataTemplate? CommitMessageTemplate { get; set; }

    public DataTemplate? InactiveRepositoryTemplate { get; set; }

    public DataTemplate? MissingBranchRepositoryTemplate { get; set; }

    public DataTemplate? ErrorTemplate { get; set; }

    public override DataTemplate? SelectTemplate(object item, DependencyObject container)
    {
        return item switch
        {
            FileContentSearchResult => FileContentTemplate,
            CommitMessageSearchResult => CommitMessageTemplate,
            InactiveRepositorySearchResult => InactiveRepositoryTemplate,
            MissingBranchRepositorySearchResult => MissingBranchRepositoryTemplate,
            ErrorSearchResult => ErrorTemplate,
            _ => base.SelectTemplate(item, container)
        };
    }
}
