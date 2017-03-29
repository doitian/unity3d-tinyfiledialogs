cmake_minimum_required(VERSION 3.7)
project(tinyfiledialogs)

add_library(tinyfiledialogs MODULE tinyfiledialogs.c)
set_target_properties(tinyfiledialogs PROPERTIES BUNDLE TRUE)
