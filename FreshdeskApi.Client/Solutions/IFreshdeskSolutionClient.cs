using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FreshdeskApi.Client.Pagination;
using FreshdeskApi.Client.Solutions.Models;
using FreshdeskApi.Client.Solutions.Requests;

namespace FreshdeskApi.Client.Solutions;

public interface IFreshdeskSolutionClient
{
    /// <summary>
    /// Retrieve all details about a single category by its id.
    ///
    /// c.f. https://developers.freshdesk.com/api/#solution_category_attributes
    /// </summary>
    /// 
    /// <param name="categoryId">
    /// The unique identifier for the category.
    /// </param>
    /// 
    /// <param name="languageCode">
    /// The language code of the language to translate the category into.
    /// Defaults to null which means don't translate.
    /// </param>
    ///
    /// <param name="cancellationToken"></param>
    ///
    /// <returns>The full group information</returns>
    Task<Category> ViewCategoryAsync(
        long categoryId,
        string? languageCode = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// List all available categories translated into the required language
    ///
    /// c.f. https://developers.freshdesk.com/api/#solution_category_attributes
    /// </summary>
    ///
    /// <param name="languageCode">
    /// The language code of the language to translate the categories into.
    ///
    /// Defaults to null which means don't translate.
    /// </param>
    /// 
    /// <param name="pagingConfiguration"></param>
    /// <param name="cancellationToken"></param>
    ///
    /// <returns>
    /// The full set of categories, this request is paged and iterating to the
    /// next entry may cause a new API call to get the next page.
    /// </returns>
    IAsyncEnumerable<Category> ListAllCategoriesAsync(
        string? languageCode = null,
        ListPaginationConfiguration? pagingConfiguration = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Delete a category from the Freshdesk instance.
    ///
    /// Note:
    /// When deleted, all translated versions will be deleted too.
    ///
    /// c.f. https://developers.freshdesk.com/api/#solution_category_attributes
    /// </summary>
    /// 
    /// <param name="categoryId">
    /// The unique group identifier.
    /// </param>
    ///
    /// <param name="cancellationToken"></param>
    Task DeleteCategoryAsync(
        long categoryId,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Update a category, changing the description, portals it's visible on or the name 
    /// </summary>
    ///
    /// <param name="categoryId">
    /// The unique identifier of the category to update.
    /// </param>
    /// 
    /// <param name="request">
    /// The object defining what updates we want to make.
    /// </param>
    ///
    /// <param name="cancellationToken"></param>
    ///
    /// <returns>The contents of the category after the update</returns>
    [Obsolete("Use " + nameof(UpdateCategoryAsync) + " with languageCode parameter instead.")]
    Task<Category> UpdateCategoryAsync(
        long categoryId,
        UpdateCategoryRequest request,
        CancellationToken cancellationToken = default) => UpdateCategoryAsync(categoryId, languageCode: null, request, cancellationToken);

    /// <summary>
    /// Update a category, changing the description, portals it's visible on or the name 
    /// </summary>
    ///
    /// <param name="categoryId">
    /// The unique identifier of the category to update.
    /// </param>
    ///
    /// <param name="languageCode">
    /// The language code (e.g. es) which the translation corresponds to.
    ///
    /// Defaults to null which means update the default language version
    /// of the category.
    /// </param>
    /// 
    /// <param name="request">
    /// The object defining what updates we want to make.
    /// </param>
    ///
    /// <param name="cancellationToken"></param>
    ///
    /// <returns>The contents of the category after the update</returns>
    [Obsolete("Use " + nameof(UpdateCategoryAsync) + " with languageCode parameter on second position instead.")]
    Task<Category> UpdateCategoryAsync(
        long categoryId,
        UpdateCategoryRequest request,
        string? languageCode = null,
        CancellationToken cancellationToken = default) => UpdateCategoryAsync(categoryId, languageCode, request, cancellationToken);

    /// <summary>
    /// Update a category, changing the description, portals it's visible on or the name 
    /// </summary>
    ///
    /// <param name="categoryId">
    /// The unique identifier of the category to update.
    /// </param>
    ///
    /// <param name="languageCode">
    /// The language code (e.g. es) which the translation corresponds to.
    ///
    /// Defaults to null which means update the default language version
    /// of the category.
    /// </param>
    /// 
    /// <param name="request">
    /// The object defining what updates we want to make.
    /// </param>
    ///
    /// <param name="cancellationToken"></param>
    ///
    /// <returns>The contents of the category after the update</returns>
    Task<Category> UpdateCategoryAsync(
        long categoryId,
        string? languageCode,
        UpdateCategoryRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Create a new category setting the name, description and portals
    /// it's visible in.
    /// </summary>
    /// 
    /// <param name="request">
    /// The object defining what updates we want to make.
    /// </param>
    ///
    /// <param name="cancellationToken"></param>
    ///
    /// <returns>The contents of the category including it's new id</returns>
    Task<Category> CreateCategoryAsync(
        CreateCategoryRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Given a category, this creates a translation of that category into the
    /// requested language code.
    /// </summary>
    /// 
    /// <param name="categoryId">
    /// The unique identifier for the category
    /// </param>
    ///
    /// <param name="languageCode">
    /// The language code (e.g. es) which the translation corresponds to.
    /// </param>
    /// 
    /// <param name="request">
    /// The object defining what updates we want to make.
    /// </param>
    ///
    /// <param name="cancellationToken"></param>
    ///
    /// <returns>The contents of the category including it's new id</returns>
    public Task<Category> CreateCategoryTranslationAsync(
        long categoryId,
        string languageCode,
        CreateCategoryRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Get a single folder from the solutions KB optionally translated
    /// into the requested language code.
    ///
    /// c.f. https://developers.freshdesk.com/api/#solution_folder_attributes
    /// </summary>
    /// 
    /// <param name="folderId">
    /// The unique identifier for the folder
    /// </param>
    ///
    /// <param name="languageCode">
    /// The language to return the folder translated into. Defaults to null
    /// which means use the default language untranslated.
    /// </param>
    ///
    /// <param name="cancellationToken"></param>
    ///
    /// <returns>The full folder information in default language</returns>
    Task<Folder> GetFolderAsync(
        long folderId,
        string? languageCode = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Return a list of all folders within a given category.
    ///
    /// Optionally translate the folders to the requested language code
    ///
    /// c.f. https://developers.freshdesk.com/api/#solution_folder_attributes
    /// </summary>
    /// 
    /// <param name="categoryId">
    /// The unique identifier for the category.
    /// </param>
    ///
    /// <param name="languageCode">
    /// The language to return the folders translated into. Defaults to null
    /// which means use the default language untranslated.
    /// </param>
    ///
    /// <param name="pagingConfiguration"></param>
    /// <param name="cancellationToken"></param>
    ///
    /// <returns>
    /// The full set of folders, this request is paged and iterating to the
    /// next entry may cause a new API call to get the next page.
    /// </returns>
    IAsyncEnumerable<Folder> GetAllFoldersInCategoryAsync(
        long categoryId,
        string? languageCode = null,
        ListPaginationConfiguration? pagingConfiguration = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Delete a folder from the solution knowledge base.
    ///
    /// Note:
    /// When deleted, all translated versions will be deleted too.
    ///
    /// c.f. https://developers.freshdesk.com/api/#solution_folder_attributes
    /// </summary>
    /// 
    /// <param name="folderId">
    /// The unique folder identifier
    /// </param>
    ///
    /// <param name="cancellationToken"></param>
    Task DeleteFolderAsync(
        long folderId,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Create a new folder within the specified category.
    ///
    /// c.f. https://developers.freshdesk.com/api/#solution_folder_attributes
    /// </summary>
    /// 
    /// <param name="categoryId">
    /// The category within which the folder will be placed
    /// </param>
    ///
    /// <param name="request">
    /// An object specifying at least the name of the new folder.
    /// </param>
    ///
    /// <param name="cancellationToken"></param>
    ///
    /// <returns>The created folder object</returns>
    Task<Folder> CreateFolderAsync(
        long categoryId,
        CreateFolderRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Given a folder, this creates a translation of that folder into the
    /// requested language code.
    ///
    /// c.f. https://developers.freshdesk.com/api/#solution_folder_attributes
    /// </summary>
    /// 
    /// <param name="folderId">
    /// The unique identifier for the folder
    /// </param>
    ///
    /// <param name="languageCode">
    /// The language code (e.g. es) which the translation corresponds to.
    /// </param>
    /// 
    /// <param name="request">
    /// Encapsulates the new translation information.
    /// </param>
    ///
    /// <param name="cancellationToken"></param>
    ///
    /// <returns>The translated folder</returns>
    Task<Folder> CreateFolderTranslationAsync(
        long folderId,
        string languageCode,
        CreateFolderRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Update a folder with new name, description, visibility etc.
    ///
    /// c.f. https://developers.freshdesk.com/api/#solution_folder_attributes
    /// </summary>
    /// 
    /// <param name="folderId">
    /// The unique identifier for the folder.
    /// </param>
    ///
    /// <param name="request">
    /// An object containing what information we want to update about the
    /// folder.
    /// </param>
    ///
    /// <param name="languageCode">
    /// The language code (e.g. es) which the translation corresponds to.
    ///
    /// Defaults to null which means update the default language version
    /// of the article.
    /// </param>
    ///
    /// <param name="cancellationToken"></param>
    ///
    /// <returns>
    /// The updated folder information (translated if the language code
    /// was set)
    /// </returns>
    Task<Folder> UpdateFolderAsync(
        long folderId,
        UpdateFolderRequest request,
        string? languageCode = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Create an article in the specified folder.
    ///
    /// c.f. https://developers.freshdesk.com/api/#solution_article_attributes
    /// </summary>
    /// 
    /// <param name="folderId">
    /// The unique identifier of the folder which the article will reside within.
    /// </param>
    ///
    /// <param name="request">
    /// The properties to set on the article.
    /// </param>
    ///
    /// <param name="cancellationToken"></param>
    ///
    /// <returns>The newly created article with ID filled in.</returns>
    Task<Article> CreateArticleAsync(
        long folderId,
        CreateArticleRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Create a translation of the specified article into the specified
    /// language.
    ///
    /// c.f. https://developers.freshdesk.com/api/#solution_article_attributes
    /// </summary>
    /// 
    /// <param name="articleId">
    /// The unique identifier for the article.
    /// </param>
    ///
    /// <param name="languageCode">
    /// The language code (e.g. es) which the translation corresponds to.
    /// </param>
    ///
    /// <param name="request">
    /// The properties of the article to set.
    /// </param>
    ///
    /// <param name="cancellationToken"></param>
    ///
    /// <returns>The newly translated article</returns>
    Task<Article> CreateArticleTranslationAsync(
        long articleId,
        string languageCode,
        CreateArticleRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Update an article (or it's translation) with the requested data.
    ///
    /// c.f. https://developers.freshdesk.com/api/#solution_article_attributes
    /// </summary>
    /// 
    /// <param name="articleId">
    /// The unique identifier for the article.
    /// </param>
    /// 
    /// <param name="request">
    /// Defines the properties to be updated.
    /// </param>
    /// 
    /// <param name="languageCode">
    /// Optional. If set then the languageCode version of the article will
    /// be updated instead of the default article.
    /// </param>
    ///
    /// <param name="cancellationToken"></param>
    ///
    /// <returns>
    /// The updated article (translated if languageCode is passed)
    /// </returns>
    [Obsolete("Use " + nameof(UpdateArticleAsync) + " with languageCode parameter on second position instead.")]
    Task<Article> UpdateArticleAsync(
        long articleId,
        UpdateArticleRequest request,
        string? languageCode = null,
        CancellationToken cancellationToken = default) => UpdateArticleAsync(articleId, languageCode, request, cancellationToken);

    /// <summary>
    /// Update an article (or it's translation) with the requested data.
    ///
    /// c.f. https://developers.freshdesk.com/api/#solution_article_attributes
    /// </summary>
    /// 
    /// <param name="articleId">
    /// The unique identifier for the article.
    /// </param>
    /// 
    /// <param name="request">
    /// Defines the properties to be updated.
    /// </param>
    /// 
    /// <param name="languageCode">
    /// Optional. If set then the languageCode version of the article will
    /// be updated instead of the default article.
    /// </param>
    ///
    /// <param name="cancellationToken"></param>
    ///
    /// <returns>
    /// The updated article (translated if languageCode is passed)
    /// </returns>
    Task<Article> UpdateArticleAsync(
        long articleId,
        string? languageCode,
        UpdateArticleRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Return all information about an article
    ///
    /// c.f. https://developers.freshdesk.com/api/#solution_article_attributes
    /// </summary>
    /// 
    /// <param name="articleId">
    /// The unique identifier for the article
    /// </param>
    ///
    /// <param name="languageCode">
    /// The language to view the article in. By default this is null which
    /// means getting the default language version.
    /// </param>
    ///
    /// <param name="cancellationToken"></param>
    ///
    /// <returns>The full article in the requested language</returns>
    Task<Article> ViewArticleAsync(
        long articleId,
        string? languageCode = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// List all articles (optionally translated) within a given folder.
    ///
    /// c.f. https://developers.freshdesk.com/api/#solution_article_attributes
    /// </summary>
    /// 
    /// <param name="folderId">
    /// The unique identifier for the folder.
    /// </param>
    /// 
    /// <param name="languageCode">
    /// The language to list the articles in. By default this is null which
    /// means getting the default language version.
    /// </param>
    ///
    /// <param name="pagingConfiguration"></param>
    /// <param name="cancellationToken"></param>
    ///
    /// <returns>
    /// The full set of articles, this request is paged and iterating to the
    /// next entry may cause a new API call to get the next page.
    /// </returns>
    IAsyncEnumerable<Article> ListArticlesInFolderAsync(
        long folderId,
        string? languageCode = null,
        ListPaginationConfiguration? pagingConfiguration = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Delete an article with all its translations.
    ///
    /// c.f. https://developers.freshdesk.com/api/#solution_article_attributes
    /// </summary>
    /// 
    /// <param name="articleId">
    /// The unique identifier of the article to delete.
    /// </param>
    /// 
    /// <param name="cancellationToken"></param>
    Task DeleteArticleAsync(
        long articleId,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Search the articles in your Freshdesk account using a keyword.
    ///
    /// c.f. https://developers.freshdesk.com/api/#solution_article_attributes
    /// </summary>
    /// 
    /// <param name="termUnencoded">
    /// The terms to search for, not yet URL encoded.
    /// </param>
    ///
    /// <param name="pagingConfiguration"></param>
    /// <param name="cancellationToken"></param>
    ///
    /// <returns>
    /// An IAsyncEnumerable which can be iterated over to return all
    /// solutions matching the term.
    /// </returns>
    IAsyncEnumerable<Article> SearchSolutionsAsync(
        string termUnencoded,
        ListPaginationConfiguration? pagingConfiguration = null,
        CancellationToken cancellationToken = default);
}
