import { createSelector, createFeatureSelector } from "@ngrx/store";
import { UserState } from "./user.reducer";


export const getUserState = createFeatureSelector<UserState>('user')

export const getUser = createSelector(
    getUserState,
    (state) => state.entity
)

export const getLoading = createSelector(
    getUserState,
    (state) => state.loading
)

export const getIsAunthorized = createSelector(
    getUserState,
    (state) => state.id
)