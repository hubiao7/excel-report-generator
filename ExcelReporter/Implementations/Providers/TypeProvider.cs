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

        private readonly Assembly _assembly;

        public TypeProvider(Assembly assembly = null)
        {
            _assembly = assembly ?? Assembly.GetExecutingAssembly();
        }

        public virtual Type GetType(string typeTemplate)
        {
            if (string.IsNullOrWhiteSpace(typeTemplate))
            {
                throw new ArgumentException(Constants.EmptyStringParamMessage, nameof(typeTemplate));
            }

            string[] typeNameParts = typeTemplate.Split(NamespaceSeparator);
            bool isNamespaceSpecified = false;
            string @namespace = null;
            string name;
            if (typeNameParts.Length == 1)
            {
                name = typeNameParts[0];
            }
            else if (typeNameParts.Length == 2)
            {
                isNamespaceSpecified = true;
                @namespace = typeNameParts[0];
                @namespace = string.IsNullOrWhiteSpace(@namespace) ? null : @namespace.Trim();
                name = typeNameParts[1];
            }
            else
            {
                throw new IncorrectTemplateException($"Type name template \"{typeTemplate}\" is incorrect");
            }

            name = name.Trim();
            IList<Type> types = (isNamespaceSpecified
                    ? _assembly.GetTypes().Where(t => t.Namespace == @namespace && t.Name == name)
                    : _assembly.GetTypes().Where(t => t.Name == name))
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
    }
}