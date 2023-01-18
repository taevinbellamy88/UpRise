﻿using UpRise.Models.Enums;

namespace UpRise.Services.Interfaces.Security
{
    /// <summary>
    /// </summary>
    /// <typeparam name="T">Type used for UserId</typeparam>
    /// <typeparam name="K">Type used for EntityId</typeparam>
    public interface ISecureEntities<T, K>
    {
        bool IsAuthorized(T userId, K entityId, EntityActionType actionType, EntityType entityType);
    }
}