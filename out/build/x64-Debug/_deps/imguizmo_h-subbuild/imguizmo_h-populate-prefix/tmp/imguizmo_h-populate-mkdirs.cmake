# Distributed under the OSI-approved BSD 3-Clause License.  See accompanying
# file Copyright.txt or https://cmake.org/licensing for details.

cmake_minimum_required(VERSION 3.5)

file(MAKE_DIRECTORY
  "C:/Users/34622/source/repos/Graphics/out/build/x64-Debug/_deps/imguizmo_h-src"
  "C:/Users/34622/source/repos/Graphics/out/build/x64-Debug/_deps/imguizmo_h-build"
  "C:/Users/34622/source/repos/Graphics/out/build/x64-Debug/_deps/imguizmo_h-subbuild/imguizmo_h-populate-prefix"
  "C:/Users/34622/source/repos/Graphics/out/build/x64-Debug/_deps/imguizmo_h-subbuild/imguizmo_h-populate-prefix/tmp"
  "C:/Users/34622/source/repos/Graphics/out/build/x64-Debug/_deps/imguizmo_h-subbuild/imguizmo_h-populate-prefix/src/imguizmo_h-populate-stamp"
  "C:/Users/34622/source/repos/Graphics/libraries/imguizmo"
  "C:/Users/34622/source/repos/Graphics/out/build/x64-Debug/_deps/imguizmo_h-subbuild/imguizmo_h-populate-prefix/src/imguizmo_h-populate-stamp"
)

set(configSubDirs )
foreach(subDir IN LISTS configSubDirs)
    file(MAKE_DIRECTORY "C:/Users/34622/source/repos/Graphics/out/build/x64-Debug/_deps/imguizmo_h-subbuild/imguizmo_h-populate-prefix/src/imguizmo_h-populate-stamp/${subDir}")
endforeach()
if(cfgdir)
  file(MAKE_DIRECTORY "C:/Users/34622/source/repos/Graphics/out/build/x64-Debug/_deps/imguizmo_h-subbuild/imguizmo_h-populate-prefix/src/imguizmo_h-populate-stamp${cfgdir}") # cfgdir has leading slash
endif()
