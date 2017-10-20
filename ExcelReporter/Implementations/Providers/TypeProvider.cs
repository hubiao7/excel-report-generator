﻿using ExcelReporter.Exceptions;
using ExcelReporter.Interfaces.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ExcelReporter.Implementations.Providers
{
    public class TypeProvider : ITypeProvider
    {
        private const char NamespaceSeparator = ':';

        private Assembly _assembly;

        public TypeProvider()
        {
        }

        public TypeProvider(Assembly assembly)
        {
            _assembly = assembly;
        }

        public virtual Type GetType(string typeTemplate)
        {
            if (string.IsNullOrWhiteSpace(typeTemplate))
            {
                throw new ArgumentException(Constants.EmptyStringParamMessage, nameof(typeTemplate));
            }

            Assembly assembly = GetAssembly();
            string[] typeNameParts = typeTemplate.Split(NamespaceSeparator);
            bool isNamespaceSpecified = false;
            string @namespace = null;
            string name;
            if (typeNameParts.Length == 1)
            {
                name = typeNameParts[0].Trim();
            }
            else if (typeNameParts.Length == 2)
            {
                isNamespaceSpecified = true;
                @namespace = typeNameParts[0].Trim();
                @namespace = string.IsNullOrWhiteSpace(@namespace) ? null : @namespace;
                name = typeNameParts[1].Trim();
            }
            else
            {
                throw new IncorrectTemplateException($"Type name template \"{typeTemplate}\" is incorrect");
            }

            IList<Type> types = (isNamespaceSpecified
                    ? assembly.GetTypes().Where(t => t.Namespace == @namespace && t.Name == name)
                    : assembly.GetTypes().Where(t => t.Name == name))
                .ToList();

            if (types.Count == 1)
            {
                return types.First();
            }
            if (!types.Any())
            {
                throw new IncorrectTemplateException($"Cannot find type by template \"{typeTemplate}\"");
            }

            throw new IncorrectTemplateException($"More than one type found by template \"{typeTemplate}\"");
        }

        private Assembly GetAssembly()
        {
            return _assembly ?? (_assembly = Assembly.GetExecutingAssembly());
        }
    }
}