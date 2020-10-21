# Changelog
All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [Unreleased]

## [0.5.6] - 2020-10-21

- Add support for merging contacts
- Add support for exporting contacts

## [0.4.1] - 2020-05-31

- Fixed serialization bug retrieving contacts with related companies from the ListContactsAsync api endpoint

## [0.4.0] - 2020-04-16

- Added support for creating agents
- Added folder visibility to the article model for when it's retrieved

## [0.3.0] - 2020-04-15
- Added support for category_name & folder_name on articles
- Added support for making contacts into agents
- Added support for updating contacts
- Added support for channel api to create notes, tickets and replies

## [0.2.0] - 2020-04-09
- Added support for ticket_fields API endpoints
- Fixed bug with paging (previously only first 30 results were ever returned)
- Switched to Newtonsoft.Json to support more JSON deserialization options
- Added CreateGroupRequest

## [0.1.0] - 2020-04-08
First public release of the package
- Solution API fully implemented
- Tickets API partially implemented
- Groups API partially implemented
- Contacts API partially implemented
- Agents API partially implemented
- Companies API partially implemented

[Unreleased]: https://github.com/DaveTCode/freshdeskapidotnet/compare/0.4.0...HEAD
[0.5.6]: https://github.com/DaveTCode/freshdeskapidotnet/releases/tag/0.5.6...0.4.0
[0.4.0]: https://github.com/DaveTCode/freshdeskapidotnet/releases/tag/0.4.0...0.3.0
[0.3.0]: https://github.com/DaveTCode/freshdeskapidotnet/releases/tag/0.3.0...0.2.0
[0.2.0]: https://github.com/DaveTCode/freshdeskapidotnet/releases/tag/0.2.0...0.1.0
[0.1.0]: https://github.com/DaveTCode/freshdeskapidotnet/releases/tag/0.1.0