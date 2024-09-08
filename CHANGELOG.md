# Changelog
All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [1.0.0] - 2024 September 04
- Initial version

## [2.0.0] - 2024 September 08

### Added
- New input system support
- Config file when using the new input system for enabling AsapUpdate to update inputs 

### Changed
- AsapUpdate is now running BEFORE update & fixedUpdate to have up-to-date inputs
- `IsInFixedUpdate` is now permanently added and not just in comments (performance concerns dispelled)